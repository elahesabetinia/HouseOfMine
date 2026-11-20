using HouseOfMine.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseOfMine.Infrastructure.Persistence.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> b)
    {
        b.HasOne(u => u.Citizen).WithOne().HasForeignKey<AppUser>(u => u.CitizenId);

        b.Property(u => u.PhoneNumber).HasMaxLength(11);
        b.HasIndex(u => u.PhoneNumber).IsUnique().HasFilter("[PhoneNumber] IS NOT NULL");
    }
}