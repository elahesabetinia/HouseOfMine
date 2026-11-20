using HouseOfMine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseOfMine.Infrastructure.Persistence.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> b)
    {
        b.Property(c => c.Name).HasMaxLength(50).IsRequired();
        b.HasOne(c => c.Province).WithMany(p => p.Cities).HasForeignKey(c => c.ProvinceId);
        b.HasIndex(c => new { c.ProvinceId, c.Name }).IsUnique();
    }
}