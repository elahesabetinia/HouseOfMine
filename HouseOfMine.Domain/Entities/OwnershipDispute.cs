using HouseOfMine.Domain.Common;
using HouseOfMine.Domain.Enums;

namespace HouseOfMine.Domain.Entities;

public class OwnershipDispute : Entity
{
    public int CitizenId { get; private set; }
    public Citizen Citizen { get; private set; } = null!;
    public int? PropertyId { get; private set; } // ملکی که اشتباه ثبت شده
    public Property? Property { get; private set; }
    public string Description { get; private set; } = null!;
    public DisputeStatus Status { get; private set; } = DisputeStatus.Pending;
    public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;

    private OwnershipDispute() { }

    public OwnershipDispute(int citizenId, string description, int? propertyId = null)
    {
        CitizenId = citizenId;
        Description = description;
        PropertyId = propertyId;
    }

    public void ChangeStatus(DisputeStatus status) => Status = status;
}