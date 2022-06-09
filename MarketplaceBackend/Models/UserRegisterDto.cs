using System.ComponentModel.DataAnnotations;

namespace MarketplaceBackend.Models
{
    public class UserRegisterDto
    {
        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]+$", ErrorMessage = "The field Password must have at least one letter and one digit.")]
        public string Password { get; set; }
    }
}
