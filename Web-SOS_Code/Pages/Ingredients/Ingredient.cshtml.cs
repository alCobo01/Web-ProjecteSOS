using Web_SOS_Code.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web_SOS_Code.Pages.Ingredients
{
    public class IngredientModel : PageModel
    {
        public Ingredient Ingredient { get; set; } = new Ingredient();
        public void OnGet()
        {
        }
    }
}
