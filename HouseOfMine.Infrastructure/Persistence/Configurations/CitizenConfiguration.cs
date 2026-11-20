using HouseOfMine.Domain.Entities;
using HouseOfMine.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseOfMine.Infrastructure.Persistence.Configurations;

public class CitizenConfiguration : IEntityTypeConfiguration<Citizen>
{
    public void Configure(EntityTypeBuilder<Citizen> b)
    {
        b.ToTable("Citizens");

        b.Property(c => c.NationalCode)
            .HasConversion(v => v.Value, v => NationalCode.Create(v))
            .HasColumnType("char(10)")
            .IsRequired();
        b.HasIndex(c => c.NationalCode).IsUnique();

        b.Property(c => c.FirstName).HasMaxLength(50).IsRequired();
        b.Property(c => c.LastName).HasMaxLength(50).IsRequired();
        b.Property(c => c.FatherName).HasMaxLength(50).IsRequired();

        b.HasOne(c => c.BirthCity).WithMany().HasForeignKey(c => c.BirthCityId);
        b.HasOne(c => c.Occupation).WithMany().HasForeignKey(c => c.OccupationId);

        b.Ignore(c => c.IsDeceased);
    }
}