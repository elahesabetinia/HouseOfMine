namespace HouseOfMine.Application.Auth;

public record AuthResult(bool Succeeded, IReadOnlyList<string> Errors)
{
    public static AuthResult Success() => new(true, Array.Empty<string>());
    public static AuthResult Fail(params string[] errors) => new(false, errors);
    public static AuthResult Fail(IEnumerable<string> errors) => new(false, errors.ToList());
}