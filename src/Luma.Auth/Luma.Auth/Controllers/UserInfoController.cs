using Luma.Auth.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using System.Security.Claims;

namespace Luma.Auth.Controllers;

public class UserinfoController(UserManager<ApplicationUser> users) : Controller
{
    private readonly UserManager<ApplicationUser> _users = users;

    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [HttpGet("/connect/userinfo"), HttpPost("/connect/userinfo")]
    public async Task<IActionResult> Userinfo()
    {
        var user = await _users.GetUserAsync(User);
        if (user is null) return Challenge();

        return Ok(new
        {
            sub = user.Id.ToString(),
            name = user.DisplayName,
            email = user.Email
        });
    }

    [Authorize(AuthenticationSchemes = "ApiAuth")]
    [HttpGet("/api/users/me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        // Extract user ID from the token claims
        var userId = User.FindFirstValue("sub") ??
                     User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

        if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var userGuid))
            return Unauthorized();

        var user = await _users.FindByIdAsync(userId);
        if (user is null) return Unauthorized();

        return Ok(new
        {
            id = user.Id,
            email = user.Email,
            displayName = user.DisplayName,
            createdAt = user.CreatedAtUtc,
            lastLoggedInAt = user.LastLoginAtUtc
        });
    }

    [Authorize(AuthenticationSchemes = "ApiAuth")]
    [HttpGet("/api/users/{userId}")]
    public async Task<IActionResult> GetUserById(string userId)
    {
        if (!Guid.TryParse(userId, out var userGuid))
            return BadRequest("Invalid user ID format");

        var user = await _users.FindByIdAsync(userId);
        if (user is null) return NotFound();

        return Ok(new
        {
            id = user.Id,
            email = user.Email,
            displayName = user.DisplayName,
            createdAt = user.CreatedAtUtc,
            lastLoggedInAt = user.LastLoginAtUtc
        });
    }
}