using HouseOfMine.Application.Auth;
using HouseOfMine.Infrastructure.Identity;
using HouseOfMine.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HouseOfMine.Application.Disputes;
using HouseOfMine.Application.Properties;
using HouseOfMine.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HouseOfMine.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddIdentity<AppUser, IdentityRole<int>>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;

            // کد ملی فقط عدد است
            options.User.AllowedUserNameCharacters = "0123456789";

            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
        })
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Account/Login";
            options.AccessDeniedPath = "/Account/Login";
        });

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPropertyService, PropertyService>();
        services.AddScoped<IDisputeService, DisputeService>();

        return services;
    }
}