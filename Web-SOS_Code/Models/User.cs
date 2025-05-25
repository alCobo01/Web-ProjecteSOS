using System.ComponentModel.DataAnnotations;

namespace Web_SOS_Code.Models
{
    public class User
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}