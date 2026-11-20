using HouseOfMine.Application.Common;
using HouseOfMine.Application.Disputes;
using HouseOfMine.Domain.Entities;
using HouseOfMine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HouseOfMine.Infrastructure.Services;

public class DisputeService : IDisputeService
{
    private readonly AppDbContext _db;

    public DisputeService(AppDbContext db) => _db = db;

    public async Task<OperationResult> CreateAsync(int userId, CreateDisputeRequest request)
    {
        var description = (request.Description ?? "").Trim();
        if (description.Length < 10)
            return OperationResult.Fail("توضیحات باید حداقل ۱۰ کاراکتر باشد.");
        if (description.Length > 1000)
            return OperationResult.Fail("توضیحات نباید بیش از ۱۰۰۰ کاراکتر باشد.");

        var citizenId = await GetCitizenIdAsync(userId);
        if (citizenId is null)
            return OperationResult.Fail("کاربر یافت نشد.");

        if (request.PropertyId is int propertyId)
        {
            var owns = await _db.Ownerships.AnyAsync(o =>
                o.CitizenId == citizenId.Value && o.PropertyId == propertyId);
            if (!owns)
                return OperationResult.Fail("ملک انتخاب‌شده در فهرست املاک شما نیست.");
        }

        _db.OwnershipDisputes.Add(
            new OwnershipDispute(citizenId.Value, description, request.PropertyId));
        await _db.SaveChangesAsync();

        return OperationResult.Success();
    }

    public async Task<IReadOnlyList<DisputeDto>> GetMyDisputesAsync(int userId)
    {
        var citizenId = await GetCitizenIdAsync(userId);
        if (citizenId is null) return Array.Empty<DisputeDto>();

        return await _db.OwnershipDisputes
            .AsNoTracking()
            .Where(d => d.CitizenId == citizenId.Value)
            .OrderByDescending(d => d.CreatedAtUtc)
            .Select(d => new DisputeDto(
                d.Id,
                d.Property != null ? d.Property.RegistrationNumber : null,
                d.Description,
                d.Status,
                d.CreatedAtUtc))
            .ToListAsync();
    }

    private Task<int?> GetCitizenIdAsync(int userId) =>
        _db.Users.Where(u => u.Id == userId)
            .Select(u => (int?)u.CitizenId)
            .FirstOrDefaultAsync();
}