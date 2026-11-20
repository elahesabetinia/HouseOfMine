using System.ComponentModel.DataAnnotations;
using HouseOfMine.Application.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HouseOfMine.Web.Pages.Account;

public class RegisterModel : PageModel
{
    private readonly IAuthService _auth;

    public RegisterModel(IAuthService auth) => _auth = auth;

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public class InputModel
    {
        [Required(ErrorMessage = "کد ملی را وارد کنید.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "کد ملی باید ۱۰ رقم باشد.")]
        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; } = "";

        [Required(ErrorMessage = "شمارهٔ موبایل را وارد کنید.")]
        [Display(Name = "شمارهٔ موبایل")]
        public string Mobile { get; set; } = "";

        [Required(ErrorMessage = "رمز عبور را وارد کنید.")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "تکرار رمز عبور را وارد کنید.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "رمز عبور و تکرار آن یکسان نیست.")]
        [Display(Name = "تکرار رمز عبور")]
        public string ConfirmPassword { get; set; } = "";
    }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var result = await _auth.RegisterAsync(
            new RegisterRequest(Input.NationalCode, Input.Mobile, Input.Password));

        if (result.Succeeded) return RedirectToPage("/Index");

        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error);
        return Page();
    }
}