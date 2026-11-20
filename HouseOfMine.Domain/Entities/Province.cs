using HouseOfMine.Domain.Common;

namespace HouseOfMine.Domain.Entities;

public class Province : Entity
{
    public string Name { get; private set; } = null!;
    public ICollection<City> Cities { get; private set; } = new List<City>();

    private Province() { }

    public Province(string name) => Name = name;
}