using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Web_SOS_Code.Models;
using Web_SOS_Code.Services;

namespace Web_SOS_Code.Pages;

public class ListIngredientsModel : PageModel
{
    private readonly IngredientService _ingredientService;

    public ListIngredientsModel(IngredientService ingredientService)
    {
        _ingredientService = ingredientService;
    }

    public List<Ingredient> Ingredients { get; set; } = new();

    [TempData]
    public string? ApiErrorMessage { get; set; }
    public bool IsAuthenticated { get; private set; }


    public async Task<IActionResult> OnGetAsync()
    {
        IsAuthenticated = User.Identity?.IsAuthenticated ?? false;
        if (!IsAuthenticated) return RedirectToPage("Login");

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

    public async Task<IActionResult> OnPostJsonAsync(IFormFile JsonFile)
    {
        if (JsonFile == null || JsonFile.Length == 0)
        {
            ApiErrorMessage = "Please select a JSON file.";
            ModelState.AddModelError(string.Empty, ApiErrorMessage);
            return Page();
        }
        try
        {
            var added = await _ingredientService.PostIngredientsJsonAsync(JsonFile);
            TempData["SuccessMessage"] = $"{added.Count} ingredients imported successfully.";
        }
        catch (HttpRequestException ex)
        {
            ApiErrorMessage = ex.Message;
            ModelState.AddModelError(string.Empty, ApiErrorMessage);
            return Page();
        }
        catch (Exception ex)
        {
            ApiErrorMessage = $"Exception calling API: {ex.Message}";
            ModelState.AddModelError(string.Empty, ApiErrorMessage);
            return Page();
        }
        return RedirectToPage();
    }
}
