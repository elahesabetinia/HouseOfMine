using HouseOfMine.Application.Common;

namespace HouseOfMine.Application.Disputes;

public interface IDisputeService
{
    Task<OperationResult> CreateAsync(int userId, CreateDisputeRequest request);
    Task<IReadOnlyList<DisputeDto>> GetMyDisputesAsync(int userId);
}