using HouseOfMine.Domain.Common;
using HouseOfMine.Domain.Enums;
using HouseOfMine.Domain.ValueObjects;

namespace HouseOfMine.Domain.Entities;

public class Citizen : Entity
{
    public NationalCode NationalCode { get; private set; } = null!;
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string FatherName { get; private set; } = null!;
    public Gender Gender { get; private set; }
    public MaritalStatus MaritalStatus { get; private set; }
    public int? OccupationId { get; private set; }
    public Occupation? Occupation { get; private set; }
    public int BirthCityId { get; private set; }
    public City BirthCity { get; private set; } = null!;
    public DateOnly BirthDate { get; private set; }
    public DateOnly? DeathDate { get; private set; }

    public ICollection<Ownership> Ownerships { get; private set; } = new List<Ownership>();

    public bool IsDeceased => DeathDate.HasValue;

    private Citizen() { }

    public Citizen(NationalCode nationalCode, string firstName, string lastName,
        string fatherName, Gender gender, MaritalStatus maritalStatus,
        int birthCityId, DateOnly birthDate, int? occupationId = null)
    {
        NationalCode = nationalCode;
        FirstName = firstName;
        LastName = lastName;
        FatherName = fatherName;
        Gender = gender;
        MaritalStatus = maritalStatus;
        BirthCityId = birthCityId;
        BirthDate = birthDate;
        OccupationId = occupationId;
    }

    public void MarkDeceased(DateOnly deathDate)
    {
        if (deathDate < BirthDate)
            throw new ArgumentException("تاریخ فوت نمی‌تواند قبل از تولد باشد.");
        DeathDate = deathDate;
    }
}
