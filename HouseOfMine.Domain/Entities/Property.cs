using HouseOfMine.Domain.Common;

namespace HouseOfMine.Domain.Entities;

public class Property : Entity
{
    public string RegistrationNumber { get; private set; } = null!; // پلاک ثبتی
    public int PropertyTypeId { get; private set; }
    public PropertyType PropertyType { get; private set; } = null!;
    public int CityId { get; private set; }
    public City City { get; private set; } = null!;
    public string Address { get; private set; } = null!;
    public string PostalCode { get; private set; } = null!;
    public decimal AreaSquareMeters { get; private set; }

    public ICollection<Ownership> Owners { get; private set; } = new List<Ownership>();

    private Property() { }

    public Property(string registrationNumber, int propertyTypeId, int cityId,
        string address, string postalCode, decimal areaSquareMeters)
    {
        RegistrationNumber = registrationNumber;
        PropertyTypeId = propertyTypeId;
        CityId = cityId;
        Address = address;
        PostalCode = postalCode;
        AreaSquareMeters = areaSquareMeters;
    }
}