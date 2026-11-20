using HouseOfMine.Domain.Common;

namespace HouseOfMine.Domain.Entities;

public class City : Entity
{
    public string Name { get; private set; } = null!;
    public int ProvinceId { get; private set; }
    public Province Province { get; private set; } = null!;

    private City() { }

    public City(string name, int provinceId)
    {
        Name = name;
        ProvinceId = provinceId;
    }
}