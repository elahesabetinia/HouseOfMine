using HouseOfMine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseOfMine.Infrastructure.Persistence.Configurations;

public class OwnershipConfiguration : IEntityTypeConfiguration<Ownership>
{
    public void Configure(EntityTypeBuilder<Ownership> b)
    {
        b.ToTable("Ownerships", t =>
            t.HasCheckConstraint("CK_Ownerships_ShareDong", "[ShareDong] BETWEEN 1 AND 6"));

        b.HasIndex(o => new { o.PropertyId, o.CitizenId }).IsUnique();

        b.HasOne(o => o.Property).WithMany(p => p.Owners).HasForeignKey(o => o.PropertyId);
        b.HasOne(o => o.Citizen).WithMany(c => c.Ownerships).HasForeignKey(o => o.CitizenId);
    }
}
