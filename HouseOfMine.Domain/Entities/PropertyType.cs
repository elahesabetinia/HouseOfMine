using HouseOfMine.Domain.Common;

namespace HouseOfMine.Domain.Entities;

public class PropertyType : Entity
{
    public string Title { get; private set; } = null!;

    private PropertyType() { }

    public PropertyType(string title) => Title = title;
}