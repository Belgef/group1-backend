using System.ComponentModel.DataAnnotations;

namespace MarketplaceBackend.Contracts.V1.Requests.Identity
{
    public class UserLoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
