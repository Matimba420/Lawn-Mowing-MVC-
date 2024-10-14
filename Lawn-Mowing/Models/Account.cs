using System.ComponentModel.DataAnnotations;

namespace Lawn_Mowing.Models
{
    public class Account
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Name { get; set; } = string.Empty; // Initialize to avoid null reference

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty; // Initialize to avoid null reference
    }
}
