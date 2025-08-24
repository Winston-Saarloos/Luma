using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpenIddict.EntityFrameworkCore.Models;

namespace Luma.Auth.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);

        b.Entity<ApplicationUser>(e =>
        {
            e.HasIndex(u => u.Email).IsUnique();
        });

        b.HasDefaultSchema("auth");

        b.UseOpenIddict();

        b.Entity<OpenIddictEntityFrameworkCoreApplication>()
            .ToTable("openiddict_applications");
        b.Entity<OpenIddictEntityFrameworkCoreAuthorization>()
            .ToTable("openiddict_authorizations");
        b.Entity<OpenIddictEntityFrameworkCoreScope>()
            .ToTable("openiddict_scopes");
        b.Entity<OpenIddictEntityFrameworkCoreToken>()
            .ToTable("openiddict_tokens");
    }
}