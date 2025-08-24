using Luma.Auth.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Security.Claims;

namespace Luma.Auth.Controllers;

public class AuthorizationController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthorizationController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet("/connect/authorize"), HttpPost("/connect/authorize")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Authorize()
    {
        var request = HttpContext.GetOpenIddictServerRequest() ?? throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return Challenge(new AuthenticationProperties { RedirectUri = Request.Path + QueryString.Create(Request.Query) });
        }

        var identity = new ClaimsIdentity(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        identity.SetClaim(OpenIddictConstants.Claims.Subject, user.Id.ToString());
        identity.SetClaim(OpenIddictConstants.Claims.Name, user.DisplayName);
        identity.SetClaim(OpenIddictConstants.Claims.Email, user.Email);

        identity.SetDestinations(claim => claim.Type switch
        {
            OpenIddictConstants.Claims.Name or OpenIddictConstants.Claims.Email
                => new[] { OpenIddictConstants.Destinations.AccessToken, OpenIddictConstants.Destinations.IdentityToken },
            _ => new[] { OpenIddictConstants.Destinations.AccessToken }
        });

        var principal = new ClaimsPrincipal(identity);
        principal.SetScopes(request.GetScopes());
        
        // Always include the API resource for API access
        var resources = new List<string> { "api" };
        if (request.GetResources().Any())
        {
            resources.AddRange(request.GetResources());
        }
        principal.SetResources(resources);

        return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

    [HttpPost("/connect/token")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Exchange()
    {
        var request = HttpContext.GetOpenIddictServerRequest() ?? throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

        if (request.IsAuthorizationCodeGrantType() || request.IsRefreshTokenGrantType())
        {
            var result = await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            var principal = result.Principal!;
            
            // Ensure API resource is always included
            principal.SetResources("api");
            
            return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
        
        if (request.IsClientCredentialsGrantType())
        {
            // Handle client credentials flow for API access
            var identity = new ClaimsIdentity(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            identity.SetClaim(OpenIddictConstants.Claims.Subject, request.ClientId);
            identity.SetClaim(OpenIddictConstants.Claims.Name, "API Client");
            
            var principal = new ClaimsPrincipal(identity);
            principal.SetScopes(request.GetScopes());
            principal.SetResources("api");
            
            return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        throw new InvalidOperationException("The specified grant type is not supported.");
    }

    [HttpGet("/connect/logout"), HttpPost("/connect/logout")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return SignOut(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

    [HttpGet("/test-token")]
    [Authorize]
    public async Task<IActionResult> GetTestToken()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return Unauthorized();
        }

        // Create the identity and principal for API access
        var identity = new ClaimsIdentity(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        identity.SetClaim(OpenIddictConstants.Claims.Subject, user.Id.ToString());
        identity.SetClaim(OpenIddictConstants.Claims.Name, user.DisplayName);
        identity.SetClaim(OpenIddictConstants.Claims.Email, user.Email);

        var principal = new ClaimsPrincipal(identity);
        principal.SetScopes("openid", "api");
        principal.SetResources("api");

        // Sign in to get the token
        await HttpContext.SignInAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme, principal);
        
        return Ok(new { 
            message = "Token generated successfully",
            userId = user.Id,
            userName = user.DisplayName,
            userEmail = user.Email,
            note = "The token is now available in your session. Use it for API calls."
        });
    }
}