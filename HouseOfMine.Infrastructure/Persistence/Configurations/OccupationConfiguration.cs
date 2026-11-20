using HouseOfMine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseOfMine.Infrastructure.Persistence.Configurations;

public class OccupationConfiguration : IEntityTypeConfiguration<Occupation>
{
    public void Configure(EntityTypeBuilder<Occupation> b)
    {
        b.Property(o => o.Title).HasMaxLength(100).IsRequired();
        b.HasIndex(o => o.Title).IsUnique();
    }
}