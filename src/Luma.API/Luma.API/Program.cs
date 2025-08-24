using Luma.API.Configuration;
using Luma.API.Domain.Entities;
using Luma.API.Infrastructure;
using Luma.API.Services;
using Luma.API.Services.EventBus;
using Luma.Common.Shared.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<HostOptions>(options =>
    {
        options.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
    });

// Add Luma Service Bus
builder.Services.AddLumaServiceBus(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LumaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("LumaDb"),
    npg => npg.MapEnum<Breakpoint>()
    )
    .UseSnakeCaseNamingConvention()
);

builder.Services.Configure<GoogleApiSettings>(
    builder.Configuration.GetSection("GoogleApis"));
builder.Services.Configure<GoogleApiSettings.WeatherSettings>(
    builder.Configuration.GetSection("GoogleApis:Weather"));
builder.Services.Configure<GoogleApiSettings.GeocodingSettings>(
    builder.Configuration.GetSection("GoogleApis:Geocoding"));

builder.Services.AddHttpClient<IWeatherService, GoogleWeatherService>();
builder.Services.AddHttpClient<IGeocodingService, GoogleGeocodingService>();
builder.Services.AddHttpClient<IUserService, UserService>();

builder.Services.AddHostedService<UserCreatedSubscriber>();

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Auth:Authority"];
        options.Audience = builder.Configuration["Auth:Audience"];
        options.RequireHttpsMetadata = false;

        // Explicitly set the metadata address
        options.MetadataAddress = "http://localhost:3001/.well-known/openid-configuration";
        
        // Configure token validation parameters
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            // Explicitly set the valid issuer
            ValidIssuer = "http://localhost:3001/",
            ValidAudience = "api",
            // Add custom validation for debugging
            ClockSkew = TimeSpan.Zero
        };
        
        // Configure metadata refresh
        options.RefreshOnIssuerKeyNotFound = true;
        options.AutomaticRefreshInterval = TimeSpan.FromMinutes(5);
        
        // Add debugging events
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"=== AUTHENTICATION FAILED ===");
                Console.WriteLine($"Exception: {context.Exception.Message}");
                Console.WriteLine($"Exception Type: {context.Exception.GetType().Name}");
                if (context.Exception.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {context.Exception.InnerException.Message}");
                }
                
                // Add more debugging for signature validation issues
                if (context.Exception is SecurityTokenSignatureKeyNotFoundException)
                {
                    Console.WriteLine($"=== SIGNATURE KEY ISSUE ===");
                    Console.WriteLine($"This usually means the JWKS endpoint is not accessible or the keys don't match");
                    Console.WriteLine($"Try visiting: http://localhost:3001/.well-known/jwks");
                }
                
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine($"=== TOKEN VALIDATED SUCCESSFULLY ===");
                Console.WriteLine($"User ID: {context.Principal?.FindFirst("sub")?.Value}");
                Console.WriteLine($"Issuer: {context.Principal?.FindFirst("iss")?.Value}");
                Console.WriteLine($"Audience: {context.Principal?.FindFirst("aud")?.Value}");
                Console.WriteLine($"Scopes: {context.Principal?.FindFirst("scope")?.Value}");
                return Task.CompletedTask;
            },
            OnChallenge = context =>
            {
                Console.WriteLine($"=== CHALLENGE ISSUED ===");
                Console.WriteLine($"Error: {context.Error}");
                Console.WriteLine($"Error Description: {context.ErrorDescription}");
                return Task.CompletedTask;
            },
            OnMessageReceived = context =>
            {
                Console.WriteLine($"=== MESSAGE RECEIVED ===");
                Console.WriteLine($"Token received: {!string.IsNullOrEmpty(context.Token)}");
                Console.WriteLine($"Authorization header: {context.Request.Headers["Authorization"]}");
                Console.WriteLine($"Token length: {context.Token?.Length ?? 0}");
                
                // Manual token extraction for debugging
                var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    var token = authHeader.Substring("Bearer ".Length).Trim();
                    Console.WriteLine($"Manual token extraction: {token.Length} characters");
                    Console.WriteLine($"Manual token starts with: {token.Substring(0, Math.Min(50, token.Length))}...");
                    
                    // Set the token manually if it wasn't extracted
                    if (string.IsNullOrEmpty(context.Token))
                    {
                        context.Token = token;
                        Console.WriteLine($"Token manually set: {!string.IsNullOrEmpty(context.Token)}");
                    }
                }
                
                if (!string.IsNullOrEmpty(context.Token))
                {
                    Console.WriteLine($"Final token starts with: {context.Token.Substring(0, Math.Min(50, context.Token.Length))}...");
                }
                return Task.CompletedTask;
            }
        };
    });

// Add configuration debugging
var authority = builder.Configuration["Auth:Authority"];
var audience = builder.Configuration["Auth:Audience"];
Console.WriteLine($"=== JWT CONFIGURATION ===");
Console.WriteLine($"Authority: {authority}");
Console.WriteLine($"Audience: {audience}");

// Test JWKS endpoint accessibility
using var httpClient = new HttpClient();
try
{
    var jwksResponse = await httpClient.GetStringAsync("http://localhost:3001/.well-known/jwks");
    Console.WriteLine($"=== JWKS ENDPOINT TEST ===");
    Console.WriteLine($"JWKS accessible: True");
    Console.WriteLine($"JWKS response length: {jwksResponse.Length} characters");
    Console.WriteLine($"JWKS starts with: {jwksResponse.Substring(0, Math.Min(100, jwksResponse.Length))}...");
}
catch (Exception ex)
{
    Console.WriteLine($"=== JWKS ENDPOINT TEST ===");
    Console.WriteLine($"JWKS accessible: False");
    Console.WriteLine($"JWKS error: {ex.Message}");
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Generates the swagger/v1/swagger.json file
    app.UseSwagger();

    // Enables the Swagger UI page
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Luma API v1");
        options.RoutePrefix = string.Empty;
    });
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
