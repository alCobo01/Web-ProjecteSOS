using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web_SOS_Code.Models;
using Web_SOS_Code.Services;

namespace Web_SOS_Code.Pages;

public class ListDishesModel : PageModel
{
    private readonly DishService _dishService;

    public ListDishesModel(DishService dishService)
    {
        _dishService = dishService;
    }

    public List<Dish> Dishes { get; set; } = new();

    [TempData]
    public string? ApiErrorMessage { get; set; }
    public bool IsAuthenticated { get; private set; }


    public async Task<IActionResult> OnGetAsync()
    {
        IsAuthenticated = User.Identity?.IsAuthenticated ?? false;
        if (!IsAuthenticated) return RedirectToPage("Index");

        try
        {
            Dishes = await _dishService.GetDishesAsync();
        }
        catch (HttpRequestException ex)
        {
            ApiErrorMessage = ex.Message;
            ModelState.AddModelError(string.Empty, ApiErrorMessage);
        }
        catch (Exception ex)
        {
            ApiErrorMessage = $"Exception calling API: {ex.Message}";
            ModelState.AddModelError(string.Empty, ApiErrorMessage);
        }

        return Page();  
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        try
        {
            await _dishService.DeleteDishAsync(id);
        }
        catch (HttpRequestException ex)
        {
            ApiErrorMessage = ex.Message;
            ModelState.AddModelError(string.Empty, ApiErrorMessage);
        }
        catch (Exception ex)
        {
            ApiErrorMessage = $"Exception calling API: {ex.Message}";
            ModelState.AddModelError(string.Empty, ApiErrorMessage);
        }
        return RedirectToPage();
    }
}
