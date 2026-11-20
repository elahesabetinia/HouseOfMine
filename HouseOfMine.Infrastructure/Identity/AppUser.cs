using HouseOfMine.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace HouseOfMine.Infrastructure.Identity;

public class AppUser : IdentityUser<int>
{
    public int CitizenId { get; set; }
    public Citizen Citizen { get; set; } = null!;
}