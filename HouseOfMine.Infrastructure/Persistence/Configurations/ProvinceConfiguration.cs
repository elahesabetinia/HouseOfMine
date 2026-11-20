using HouseOfMine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseOfMine.Infrastructure.Persistence.Configurations;

public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> b)
    {
        b.Property(p => p.Name).HasMaxLength(50).IsRequired();
        b.HasIndex(p => p.Name).IsUnique();
    }
}