using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using HouseOfMine.Application.Disputes;
using HouseOfMine.Application.Properties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HouseOfMine.Web.Pages;

public class IndexModel : PageModel
{
    private readonly IPropertyService _properties;
    private readonly IDisputeService _disputes;

    public IndexModel(IPropertyService properties, IDisputeService disputes)
    {
        _properties = properties;
        _disputes = disputes;
    }

    public IReadOnlyList<OwnedPropertyDto> Properties { get; private set; } = Array.Empty<OwnedPropertyDto>();
    public IReadOnlyList<DisputeDto> Disputes { get; private set; } = Array.Empty<DisputeDto>();

    [BindProperty]
    public DisputeInput Input { get; set; } = new();

    [TempData]
    public string? Message { get; set; }

    public class DisputeInput
    {
        [Display(Name = "ملک مورد نظر")]
        public int? PropertyId { get; set; }

        [Required(ErrorMessage = "توضیحات را وارد کنید.")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "توضیحات باید بین ۱۰ تا ۱۰۰۰ کاراکتر باشد.")]
        [Display(Name = "توضیحات")]
        public string Description { get; set; } = "";
    }

    private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    public async Task OnGetAsync() => await LoadAsync();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadAsync();
            return Page();
        }

        var result = await _disputes.CreateAsync(
            UserId, new CreateDisputeRequest(Input.PropertyId, Input.Description));

        if (result.Succeeded)
        {
            Message = "درخواست شما ثبت شد و توسط پشتیبانی بررسی می‌شود.";
            return RedirectToPage();
        }

        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error);

        await LoadAsync();
        return Page();
    }

    private async Task LoadAsync()
    {
        Properties = await _properties.GetMyPropertiesAsync(UserId);
        Disputes = await _disputes.GetMyDisputesAsync(UserId);
    }
}