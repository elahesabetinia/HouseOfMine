using System.ComponentModel.DataAnnotations;
using HouseOfMine.Application.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HouseOfMine.Web.Pages.Account;

public class LoginModel : PageModel
{
    private readonly IAuthService _auth;

    public LoginModel(IAuthService auth) => _auth = auth;

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public class InputModel
    {
        [Required(ErrorMessage = "کد ملی را وارد کنید.")]
        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; } = "";

        [Required(ErrorMessage = "رمز عبور را وارد کنید.")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; } = "";

        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var result = await _auth.LoginAsync(Input.NationalCode, Input.Password, Input.RememberMe);
        if (result.Succeeded) return RedirectToPage("/Index");

        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error);
        return Page();
    }
}