using System.ComponentModel.DataAnnotations;

namespace TechnoBit.DTOs
{
    public class RegisterDTO
    {
        
        [Required(ErrorMessage = "Username is required")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Username must be at least {2} characters long and at max {1} characters long.")]
        [RegularExpression(@"^[a-z0-9]+$", ErrorMessage = "Username can only contain lowercase letters and numbers")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        [StringLength(16, ErrorMessage = "The password must be at least {2} characters long and at max {1} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "The password must contain at least one uppercase letter, one lowercase letter, and one number.")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
    }
}