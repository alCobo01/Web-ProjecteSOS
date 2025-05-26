using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web_SOS_Code.Models;
using Web_SOS_Code.Services;

namespace Web_SOS_Code.Pages;

public class AddIngredientModel : PageModel
{
    private readonly IngredientService _ingredientService;

    public AddIngredientModel(IngredientService ingredientService)
    {
        _ingredientService = ingredientService;
    }

    [BindProperty]
    public Ingredient Ingredient { get; set; } = new Ingredient();


    [TempData]
    public string? ApiErrorMessage { get; set; }
    public bool IsAuthenticated { get; private set; }

    public void OnGet()
    {
        IsAuthenticated = User.Identity?.IsAuthenticated ?? false;
        if (!IsAuthenticated) RedirectToPage("Index");

        TempData.Clear();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        try
        {
            await _ingredientService.PostIngredientAsync(Ingredient);
            TempData["SuccessMessage"] = $"Ingredient {Ingredient.Name} afegit correctament!";
            return RedirectToPage("ListIngredient");
        }
        catch (HttpRequestException ex)
        {
            ApiErrorMessage = ex.Message;
            return Page();
        }
        catch (Exception ex)
        {
            ApiErrorMessage = $"Unexpected error: {ex.Message}";
            return Page();
        }
    }
}
