using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_SOS_Code.Models;
using Web_SOS_Code.Models.DTOs;
using Web_SOS_Code.Services;

namespace Web_SOS_Code.Pages;

public class EditDishModel : PageModel
{
    private readonly DishService _dishService;
    private readonly IngredientService _ingredientService;

    public EditDishModel(IngredientService ingredientService, DishService dishService)
    {
        _ingredientService = ingredientService;
        _dishService = dishService;
    }

    [BindProperty]
    public Dish Dish { get; set; } = new Dish();

    [BindProperty]
    public List<string> SelectedIngredientsName { get; set; } = new List<string>();

    public IngredientOptions IngredientOptions { get; set; } = new();

    [TempData]
    public string? ApiErrorMessage { get; set; }
    public bool IsAuthenticated { get; private set; }

    public async Task<IActionResult> OnGet(int id)
    {
        IsAuthenticated = User.Identity?.IsAuthenticated ?? false;
        if (!IsAuthenticated) RedirectToPage("Index");
        TempData.Clear();

        Dish = await _dishService.GetDishByIdAsync(id);

        if (Dish == null)
        {
            ApiErrorMessage = "Dish not found.";
            return RedirectToPage("ListDish");
        }

        SelectedIngredientsName = Dish.IngredientsName ?? new List<string>();

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
        if (!ModelState.IsValid) return Page();

        try
        {
            var updateDish = new UpdateDishDTO
            {
                Name = Dish.Name,
                Description = Dish.Description,
                ImageUrl = Dish.ImageUrl,
                Price = Dish.Price,
                IngredientsName = SelectedIngredientsName
            };

            await _dishService.PutDishAsync(Dish.Id, updateDish);
            TempData["SuccessMessage"] = $"Plat {Dish.Name} editat correctament!";
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
