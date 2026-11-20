namespace HouseOfMine.Application.Properties;

public interface IPropertyService
{
    Task<IReadOnlyList<OwnedPropertyDto>> GetMyPropertiesAsync(int userId);
}