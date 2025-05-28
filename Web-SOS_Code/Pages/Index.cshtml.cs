using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web_SOS_Code.Models;
using Web_SOS_Code.Services.Auth;

namespace Web_SOS_Code.Pages;

public class IndexModel : PageModel
{
    private readonly AuthService _authService;

    public IndexModel(AuthService authService)
    {
        _authService = authService;
    }

    [BindProperty]
    public new User User { get; set; }

    [TempData]
    public string ErrorMessage { get; set; }

    public bool IsAuthenticated { get; private set; }

    public IActionResult OnGet()
    {
        IsAuthenticated = HttpContext.User.Identity?.IsAuthenticated ?? false;
        if (IsAuthenticated)
            return RedirectToPage("ListDishes");

        TempData.Clear();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
                return Page();
        
        var result = await _authService.LoginAsync(User);
        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return Page();
        }

        TempData["SuccessMessage"] = "Login successful! Redirecting...";
        return Page();
    }
}
