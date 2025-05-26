using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web_SOS_Code.Models
{
    public class IngredientOptions
    {
        [Required(ErrorMessage = "Selecciona un ingredient")]
        [MinLength(1, ErrorMessage = "Selecciona com a mínim un ingredient")]
        public List<SelectListItem> Options { get; set; } = new();
    }
}
