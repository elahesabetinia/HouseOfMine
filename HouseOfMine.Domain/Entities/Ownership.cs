using HouseOfMine.Domain.Common;

namespace HouseOfMine.Domain.Entities;

public class Ownership : Entity
{
    public int PropertyId { get; private set; }
    public Property Property { get; private set; } = null!;
    public int CitizenId { get; private set; }
    public Citizen Citizen { get; private set; } = null!;
    public byte ShareDong { get; private set; } // از ۶ دانگ
    public DateOnly FromDate { get; private set; }

    private Ownership() { }

    public Ownership(int propertyId, int citizenId, byte shareDong, DateOnly fromDate)
    {
        if (shareDong is < 1 or > 6)
            throw new ArgumentOutOfRangeException(nameof(shareDong), "سهم باید بین ۱ تا ۶ دانگ باشد.");

        PropertyId = propertyId;
        CitizenId = citizenId;
        ShareDong = shareDong;
        FromDate = fromDate;
    }
}