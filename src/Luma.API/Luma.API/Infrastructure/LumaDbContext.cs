using Luma.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Luma.API.Infrastructure;

public class LumaDbContext : DbContext
{
    public LumaDbContext(DbContextOptions<LumaDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Dashboard> Dashboards => Set<Dashboard>();
    public DbSet<DashboardLayout> DashboardLayouts => Set<DashboardLayout>();
    public DbSet<Widget> Widgets => Set<Widget>();
    public DbSet<WidgetInstance> WidgetInstances => Set<WidgetInstance>();
    public DbSet<WidgetConfig> WidgetConfigs => Set<WidgetConfig>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        // Users
        b.Entity<User>().ToTable("users");
        b.Entity<User>().HasIndex(x => x.Email).IsUnique();
        b.Entity<User>().Property(x => x.Email).HasMaxLength(320).IsRequired();
        b.Entity<User>().Property(x => x.DisplayName).HasMaxLength(120).IsRequired();

        // Dashboards
        b.Entity<Dashboard>().ToTable("dashboards");
        b.Entity<Dashboard>().HasIndex(x => x.UserId);
        b.Entity<Dashboard>().Property(x => x.Name).HasMaxLength(120).IsRequired();
        b.Entity<Dashboard>()
            .HasOne(d => d.User).WithMany(u => u.Dashboards)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Breakpoint enum (Postgres native enum if using Npgsql)
        b.HasPostgresEnum<Breakpoint>(); // requires Npgsql; ok to keep as int if you prefer

        // DashboardLayouts
        b.Entity<DashboardLayout>().ToTable("dashboard_layouts");
        b.Entity<DashboardLayout>()
            .HasIndex(x => new { x.DashboardId, x.Bp })
            .IsUnique();
        b.Entity<DashboardLayout>()
            .HasOne(l => l.Dashboard).WithMany(d => d.Layouts)
            .HasForeignKey(l => l.DashboardId)
            .OnDelete(DeleteBehavior.Cascade);

        // Widgets (catalog)
        b.Entity<Widget>().ToTable("widgets");
        b.Entity<Widget>().HasIndex(w => w.TypeKey).IsUnique();
        b.Entity<Widget>().Property(w => w.TypeKey).HasMaxLength(64).IsRequired();
        b.Entity<Widget>().Property(w => w.DisplayName).HasMaxLength(120).IsRequired();

        // WidgetInstances
        b.Entity<WidgetInstance>().ToTable("widget_instances");
        b.Entity<WidgetInstance>().HasIndex(i => i.DashboardId);
        b.Entity<WidgetInstance>()
            .HasOne(i => i.Dashboard).WithMany(d => d.WidgetInstances)
            .HasForeignKey(i => i.DashboardId)
            .OnDelete(DeleteBehavior.Cascade);
        b.Entity<WidgetInstance>()
            .HasOne(i => i.Widget).WithMany()
            .HasForeignKey(i => i.WidgetId)
            .OnDelete(DeleteBehavior.Restrict);

        // WidgetConfig (1:1 PK=FK)
        b.Entity<WidgetConfig>().ToTable("widget_configs");
        b.Entity<WidgetConfig>().HasKey(c => c.WidgetInstanceId);
        b.Entity<WidgetConfig>()
            .HasOne(c => c.WidgetInstance).WithOne(i => i.Config)
            .HasForeignKey<WidgetConfig>(c => c.WidgetInstanceId);

        b.Entity<User>()
            .Property(x => x.LastLoggedInAt)
            .HasColumnType("timestamp with time zone");

        // Optional: map JSONB
        // b.Entity<WidgetConfig>().Property(c => c.Config).HasColumnType("jsonb");
    }
}
