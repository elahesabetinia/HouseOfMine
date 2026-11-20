using HouseOfMine.Application.Properties;
using HouseOfMine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HouseOfMine.Infrastructure.Services;

public class PropertyService : IPropertyService
{
    private readonly AppDbContext _db;

    public PropertyService(AppDbContext db) => _db = db;

    public async Task<IReadOnlyList<OwnedPropertyDto>> GetMyPropertiesAsync(int userId)
    {
        var citizenId = await _db.Users
            .Where(u => u.Id == userId)
            .Select(u => (int?)u.CitizenId)
            .FirstOrDefaultAsync();

        if (citizenId is null) return Array.Empty<OwnedPropertyDto>();

        return await _db.Ownerships
            .AsNoTracking()
            .Where(o => o.CitizenId == citizenId.Value)
            .OrderBy(o => o.Property.RegistrationNumber)
            .Select(o => new OwnedPropertyDto(
                o.PropertyId,
                o.Property.RegistrationNumber,
                o.Property.PropertyType.Title,
                o.Property.City.Province.Name,
                o.Property.City.Name,
                o.Property.Address,
                o.Property.PostalCode,
                o.Property.AreaSquareMeters,
                o.ShareDong))
            .ToListAsync();
    }
}