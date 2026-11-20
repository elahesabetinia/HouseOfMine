using HouseOfMine.Application.Auth;
using HouseOfMine.Domain.ValueObjects;
using HouseOfMine.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HouseOfMine.Infrastructure.Identity;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly AppDbContext _db;

    public AuthService(UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager, AppDbContext db)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _db = db;
    }

    public async Task<AuthResult> RegisterAsync(RegisterRequest request)
    {
        var nationalCode = (request.NationalCode ?? "").Trim();
        var mobile = (request.Mobile ?? "").Trim();

        if (!NationalCode.IsValid(nationalCode))
            return AuthResult.Fail("کد ملی نامعتبر است.");

        if (mobile.Length != 11 || !mobile.StartsWith("09") || !mobile.All(char.IsAsciiDigit))
            return AuthResult.Fail("شمارهٔ موبایل باید ۱۱ رقم و با ۰۹ شروع شود.");

        var code = NationalCode.Create(nationalCode);
        var citizen = await _db.Citizens.FirstOrDefaultAsync(c => c.NationalCode == code);
        if (citizen is null)
            return AuthResult.Fail("شهروندی با این کد ملی در سامانه یافت نشد.");

        if (citizen.IsDeceased)
            return AuthResult.Fail("امکان ثبت‌نام برای این کد ملی وجود ندارد.");

        if (await _db.Users.AnyAsync(u => u.CitizenId == citizen.Id))
            return AuthResult.Fail("برای این کد ملی قبلاً ثبت‌نام انجام شده است.");

        if (await _db.Users.AnyAsync(u => u.PhoneNumber == mobile))
            return AuthResult.Fail("این شمارهٔ موبایل قبلاً استفاده شده است.");

        var user = new AppUser
        {
            UserName = nationalCode,
            PhoneNumber = mobile,
            CitizenId = citizen.Id
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            return AuthResult.Fail(result.Errors.Select(e => e.Description));

        await _signInManager.SignInAsync(user, isPersistent: false);
        return AuthResult.Success();
    }

    public async Task<AuthResult> LoginAsync(string nationalCode, string password, bool rememberMe)
    {
        var result = await _signInManager.PasswordSignInAsync(
            (nationalCode ?? "").Trim(), password, rememberMe, lockoutOnFailure: true);

        if (result.Succeeded) return AuthResult.Success();
        if (result.IsLockedOut) return AuthResult.Fail("حساب شما موقتاً قفل شده است. بعداً دوباره تلاش کنید.");
        return AuthResult.Fail("کد ملی یا رمز عبور اشتباه است.");
    }

    public Task LogoutAsync() => _signInManager.SignOutAsync();
}