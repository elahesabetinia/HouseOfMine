using HouseOfMine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseOfMine.Infrastructure.Persistence.Configurations;

public class PropertyTypeConfiguration : IEntityTypeConfiguration<PropertyType>
{
    public void Configure(EntityTypeBuilder<PropertyType> b)
    {
        b.Property(t => t.Title).HasMaxLength(50).IsRequired();
        b.HasIndex(t => t.Title).IsUnique();
    }
}