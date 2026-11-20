using HouseOfMine.Application.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HouseOfMine.Web.Pages.Account;

public class LogoutModel : PageModel
{
    private readonly IAuthService _auth;

    public LogoutModel(IAuthService auth) => _auth = auth;

    public IActionResult OnGet() => RedirectToPage("/Index");

    public async Task<IActionResult> OnPostAsync()
    {
        await _auth.LogoutAsync();
        return RedirectToPage("/Account/Login");
    }
}