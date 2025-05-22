using Web_SOS_Code.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web_SOS_Code.Pages.Dishs
{
    public class AddDishModel : PageModel
    {
        public Dish Dish { get; set; } = new Dish();
        public void OnGet()
        {

        }
    }
}