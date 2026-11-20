using HouseOfMine.Domain.Entities;
using HouseOfMine.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace HouseOfMine.Infrastructure.Persistence;

public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Citizen> Citizens => Set<Citizen>();
    public DbSet<Province> Provinces => Set<Province>();
    public DbSet<City> Cities => Set<City>();
    public DbSet<Occupation> Occupations => Set<Occupation>();
    public DbSet<PropertyType> PropertyTypes => Set<PropertyType>();
    public DbSet<Property> Properties => Set<Property>();
    public DbSet<Ownership> Ownerships => Set<Ownership>();
    public DbSet<OwnershipDispute> OwnershipDisputes => Set<OwnershipDispute>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // جلوگیری از خطای Multiple Cascade Paths و حذف ناخواستهٔ زنجیره‌ای
        foreach (var fk in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            fk.DeleteBehavior = DeleteBehavior.Restrict;
    }
}