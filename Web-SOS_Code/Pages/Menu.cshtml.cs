using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web_SOS_Code.Models;
using Web_SOS_Code.Services.Auth;

namespace Web_SOS_Code.Pages;

public class MenuModel : PageModel
{
    public bool IsAuthenticated { get; set; }

    public IActionResult OnGet()
    {
        IsAuthenticated = HttpContext.User.Identity?.IsAuthenticated ?? false;
        if (!IsAuthenticated)
            return RedirectToPage("Index");

        return Page();
    }
}
