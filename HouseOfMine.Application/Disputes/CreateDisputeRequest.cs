namespace HouseOfMine.Application.Disputes;

public record CreateDisputeRequest(int? PropertyId, string Description);