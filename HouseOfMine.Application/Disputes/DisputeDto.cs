using HouseOfMine.Domain.Enums;

namespace HouseOfMine.Application.Disputes;

public record DisputeDto(
    int Id,
    string? PropertyRegistrationNumber,
    string Description,
    DisputeStatus Status,
    DateTime CreatedAtUtc);