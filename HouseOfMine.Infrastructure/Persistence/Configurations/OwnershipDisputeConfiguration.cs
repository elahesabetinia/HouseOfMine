using HouseOfMine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseOfMine.Infrastructure.Persistence.Configurations;

public class OwnershipDisputeConfiguration : IEntityTypeConfiguration<OwnershipDispute>
{
    public void Configure(EntityTypeBuilder<OwnershipDispute> b)
    {
        b.ToTable("OwnershipDisputes");

        b.Property(d => d.Description).HasMaxLength(1000).IsRequired();

        b.HasOne(d => d.Citizen).WithMany().HasForeignKey(d => d.CitizenId);
        b.HasOne(d => d.Property).WithMany().HasForeignKey(d => d.PropertyId);
    }
}