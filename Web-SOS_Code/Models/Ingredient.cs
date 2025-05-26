using System.ComponentModel.DataAnnotations;

namespace Web_SOS_Code.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nom de l'ingredient és obligatori")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "La quantitat de l'ingredient és obligatòria")]
        [Range(1, int.MaxValue, ErrorMessage = "La quantitat ha de ser un nombre enter positiu")]
        public int Quantity { get; set; }

        [DataType(DataType.Date)]
        [CustomValidation(typeof(Ingredient), nameof(ValidateExpirationDate))]
        public DateTime ExpirationDate { get; set; }

        //Method to validate the expiration date to ensure it is not in the past
        //It must be public
        public static ValidationResult? ValidateExpirationDate(DateTime expirationDate, ValidationContext context)
        {
            if (expirationDate.Date < DateTime.Today)
            {
                return new ValidationResult("The expiration date cannot be earlier than today.");
            }
            return ValidationResult.Success;
        }
    }
}