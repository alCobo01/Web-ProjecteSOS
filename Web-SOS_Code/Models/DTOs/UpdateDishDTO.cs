using System.ComponentModel.DataAnnotations;

namespace Web_SOS_Code.Models.DTOs
{
    public class UpdateDishDTO
    {
        [Required(ErrorMessage = "El nom del plat és obligatori")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripció del plat és obligatòria")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "La URL de l'imatge del plat és obligatòria")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "El preu del plat és obligatori")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El preu ha de ser un nombre positiu")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Selecciona com a mínim un ingredient")]
        [MinLength(1, ErrorMessage = "Selecciona com a mínim un ingredient")]
        public List<string> IngredientsName { get; set; } = new List<string>();
    }
}
