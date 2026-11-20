namespace HouseOfMine.Application.Common;

public record OperationResult(bool Succeeded, IReadOnlyList<string> Errors)
{
    public static OperationResult Success() => new(true, Array.Empty<string>());
    public static OperationResult Fail(params string[] errors) => new(false, errors);
}