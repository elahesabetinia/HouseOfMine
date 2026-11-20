using HouseOfMine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseOfMine.Infrastructure.Persistence.Configurations;

public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> b)
    {
        b.ToTable("Properties");

        b.Property(p => p.RegistrationNumber).HasMaxLength(30).IsRequired();
        b.HasIndex(p => p.RegistrationNumber).IsUnique();
        b.Property(p => p.Address).HasMaxLength(300).IsRequired();
        b.Property(p => p.PostalCode).HasColumnType("char(10)").IsRequired();
        b.Property(p => p.AreaSquareMeters).HasPrecision(10, 2);

        b.HasOne(p => p.PropertyType).WithMany().HasForeignKey(p => p.PropertyTypeId);
        b.HasOne(p => p.City).WithMany().HasForeignKey(p => p.CityId);
    }
}