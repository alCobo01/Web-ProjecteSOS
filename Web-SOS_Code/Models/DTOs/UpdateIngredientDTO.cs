using System.ComponentModel.DataAnnotations;

namespace Web_SOS_Code.Models.DTOs
{
    public class UpdateIngredientDTO
    {
        [Required(ErrorMessage = "El nom de l'ingredient és obligatori")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "La data de caducitat de l'ingredient és obligatòria")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(UpdateIngredientDTO), nameof(ValidateExpirationDate))]
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
