using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_SOS_Code.Models;
using Web_SOS_Code.Services;

namespace Web_SOS_Code.Pages;

public class AddDishModel : PageModel
{
    private readonly DishService _dishService;
    private readonly IngredientService _ingredientService;

    public AddDishModel(DishService dishService, IngredientService ingredientService)
    {
        _dishService = dishService;
        _ingredientService = ingredientService;
    }

    [BindProperty]
    public Dish Dish { get; set; } = new Dish();

    [BindProperty]
    public List<string> SelectedIngredientsName { get; set; } = new List<string>();

    public IngredientOptions IngredientOptions { get; set; } = new();


    [TempData]
    public string? ApiErrorMessage { get; set; }
    public bool IsAuthenticated { get; private set; }

    public async Task<IActionResult> OnGet()
    {
        IsAuthenticated = User.Identity?.IsAuthenticated ?? false;
        if (!IsAuthenticated) RedirectToPage("Index");

        TempData.Clear();

        var ingredientsNameList = await _ingredientService.GetIngredientsName();
        IngredientOptions.Options = ingredientsNameList
            .Select(i => new SelectListItem
                {
                    Value = i,
                    Text = i,
                    Selected = SelectedIngredientsName.Contains(i)
            })
            .ToList();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        ModelState.Clear();
        Dish.IngredientsName = SelectedIngredientsName;
        TryValidateModel(Dish);
        if (!ModelState.IsValid) return Page();
        
        try
        {
            await _dishService.PostDishAsync(Dish);
            TempData["SuccessMessage"] = $"Plat {Dish.Name} afegit correctament!";
            return RedirectToPage("ListDishes");
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
