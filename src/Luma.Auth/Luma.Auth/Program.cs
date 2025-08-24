using Luma.Auth.Data;
using Luma.Auth.Services;
using Luma.Common.Shared.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.Configure<HostOptions>(options =>
{
    options.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
});

// DB
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseNpgsql(config.GetConnectionString("LumaAuthDb"))
       .UseSnakeCaseNamingConvention();
    opt.UseOpenIddict();
});

// Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(opt =>
{
    opt.User.RequireUniqueEmail = true;
    opt.SignIn.RequireConfirmedEmail = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(o =>
{
    o.LoginPath = "/Account/Login";
    o.LogoutPath = "/Account/Logout";
    o.AccessDeniedPath = "/Account/AccessDenied";
});

// Add Luma Service Bus
builder.Services.AddLumaServiceBus(builder.Configuration);

// Register event publishers
builder.Services.AddScoped<IUserEventsPublisher, UserEventsPublisher>();

// Razor Pages
builder.Services.AddRazorPages();

// OpenIddict
builder.Services.AddOpenIddict()
    .AddCore(opt =>
    {
        opt.UseEntityFrameworkCore()
           .UseDbContext<AppDbContext>();
    })
    .AddServer(opt =>
    {
        opt.AllowAuthorizationCodeFlow()
           .RequireProofKeyForCodeExchange()
           .AllowRefreshTokenFlow()
           .AllowClientCredentialsFlow(); // Add client credentials flow - for generating test tokens without the web app

        opt.SetAccessTokenLifetime(TimeSpan.FromMinutes(60))
           //.SetAuthorizationCodeLifetime(TimeSpan.FromMinutes(5))
           .SetRefreshTokenLifetime(TimeSpan.FromDays(30));

        opt.SetAuthorizationEndpointUris("/connect/authorize")
           .SetTokenEndpointUris("/connect/token")
           .SetUserInfoEndpointUris("/connect/userinfo")
           .SetEndSessionEndpointUris("/connect/logout");

        opt.RegisterScopes(OpenIddictConstants.Scopes.OpenId,
            OpenIddictConstants.Scopes.Email,
            OpenIddictConstants.Scopes.Profile,
            OpenIddictConstants.Scopes.OfflineAccess,
            "api");

        opt.AddDevelopmentEncryptionCertificate()
           .AddDevelopmentSigningCertificate();

        // Use compact JWT serialization for access tokens
        opt.DisableAccessTokenEncryption();

        opt.UseAspNetCore()
            .EnableAuthorizationEndpointPassthrough()
            .EnableTokenEndpointPassthrough()
            .EnableUserInfoEndpointPassthrough()
            .EnableEndSessionEndpointPassthrough()
#if DEBUG
            .DisableTransportSecurityRequirement() // v7: only available on Server ASP.NET Core builder
#endif
            ;
    })
    .AddValidation(opt =>
    {
        opt.UseLocalServer();
        opt.UseAspNetCore();
    });

builder.Services.AddAuthentication()
    .AddJwtBearer("ApiAuth", options =>
    {
        options.Authority = "http://localhost:3001";
        options.Audience = "api";
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "http://localhost:3001/",
            ValidAudience = "api"
        };
    });

// CORS
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("LumaFrontend", p =>
        p.WithOrigins("http://localhost:3000", "https://localhost:3000")
         .AllowAnyHeader()
         .AllowAnyMethod()
         .AllowCredentials());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

await SeedOpenIddictAsync(app);

app.UseCors("LumaFrontend");
app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();

static async Task SeedOpenIddictAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();

    var appManager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

    if (await appManager.FindByClientIdAsync("webclient") is null)
    {
        await appManager.CreateAsync(new OpenIddictApplicationDescriptor
        {
            ClientId = "webclient",
            DisplayName = "Next.js frontend",
            ClientType = OpenIddictConstants.ClientTypes.Public,
            RedirectUris = { new Uri("http://localhost:3000/api/auth/callback/luma") },
            PostLogoutRedirectUris = { new Uri("http://localhost:3000") },
            Permissions =
            {
                OpenIddictConstants.Permissions.Prefixes.Endpoint + "authorization",
                OpenIddictConstants.Permissions.Prefixes.Endpoint + "token",
                OpenIddictConstants.Permissions.Prefixes.Endpoint + "logout",
                OpenIddictConstants.Permissions.Prefixes.Endpoint + "userinfo",
                OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                OpenIddictConstants.Permissions.GrantTypes.ClientCredentials, // Add client credentials
                OpenIddictConstants.Permissions.ResponseTypes.Code,
                OpenIddictConstants.Permissions.Prefixes.Scope + OpenIddictConstants.Scopes.Email,
                OpenIddictConstants.Permissions.Prefixes.Scope + OpenIddictConstants.Scopes.Profile,
                OpenIddictConstants.Permissions.Prefixes.Scope + OpenIddictConstants.Scopes.OfflineAccess,
                OpenIddictConstants.Permissions.Prefixes.Scope + "api", // Add API scope permission
                OpenIddictConstants.Permissions.Prefixes.Resource + "api" // Add API resource permission
            },
            Requirements =
            {
                OpenIddictConstants.Requirements.Features.ProofKeyForCodeExchange
            }
        });
    }
}