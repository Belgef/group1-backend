using System.ComponentModel.DataAnnotations;

namespace MarketplaceBackend.Contracts.V1.Requests.Identity
{
    public class RefreshTokenRequest
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
