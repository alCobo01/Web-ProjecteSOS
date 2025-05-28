using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web_SOS_Code.Models;
using Web_SOS_Code.Models.DTOs;
using Web_SOS_Code.Services;

namespace Web_SOS_Code.Pages;

public class EditIngredientModel : PageModel
{
    private readonly IngredientService _ingredientService;

    public EditIngredientModel(IngredientService ingredientService)
    {
        _ingredientService = ingredientService;
    }

    [BindProperty]
    public Ingredient Ingredient { get; set; } = new Ingredient();


    [TempData]
    public string? ApiErrorMessage { get; set; }
    public bool IsAuthenticated { get; private set; }

    public async Task<IActionResult> OnGet(int id)
    {
        IsAuthenticated = User.Identity?.IsAuthenticated ?? false;
        if (!IsAuthenticated) RedirectToPage("Index");
        TempData.Clear();

        Ingredient = await _ingredientService.GetIngredientByIdAsync(id);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.TryGetValue("Ingredient.Name", out var entryName) && entryName.Errors.Count > 0)
        {
            return Page();
        }

        if (ModelState.TryGetValue("Ingredient.ExpirationDate", out var entryExpirationDate) && entryExpirationDate.Errors.Count > 0)
        {
            return Page();
        }

        try
        {
            var updateIngredient = new UpdateIngredientDTO
            {
                Name = Ingredient.Name,
                ExpirationDate = Ingredient.ExpirationDate
            };

            await _ingredientService.PutIngredientAsync(Ingredient.Id, updateIngredient);
            TempData["SuccessMessage"] = $"Ingredient {Ingredient.Name} editat correctament!";
            return RedirectToPage("ListIngredients");
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
