namespace HouseOfMine.Application.Properties;

public record OwnedPropertyDto(
    int PropertyId,
    string RegistrationNumber,
    string PropertyType,
    string Province,
    string City,
    string Address,
    string PostalCode,
    decimal AreaSquareMeters,
    byte ShareDong);