using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web_SOS_Code.Models;
using Web_SOS_Code.Services;

namespace Web_SOS_Code.Pages;

public class ListIngredientModel : PageModel
{
    private readonly IngredientService _ingredientService;

    public ListIngredientModel(IngredientService ingredientService)
    {
        _ingredientService = ingredientService;
    }

    public List<Ingredient> Ingredients { get; set; } = new();

    [TempData]
    public string? ApiErrorMessage { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            Ingredients = await _ingredientService.GetIngredientsAsync();
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
            await _ingredientService.DeleteIngredientAsync(id);
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
