namespace HouseOfMine.Application.Auth;

public interface IAuthService
{
    Task<AuthResult> RegisterAsync(RegisterRequest request);
    Task<AuthResult> LoginAsync(string nationalCode, string password, bool rememberMe);
    Task LogoutAsync();
}