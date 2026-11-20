using HouseOfMine.Domain.Common;

namespace HouseOfMine.Domain.Entities;

public class Occupation : Entity
{
    public string Title { get; private set; } = null!;

    private Occupation() { }

    public Occupation(string title) => Title = title;
}