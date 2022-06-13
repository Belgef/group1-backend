using MarketplaceBackend.Models;

namespace MarketplaceBackend.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(UserRegistrationRequest user);
        Task<AuthenticationResult> LoginAsync(string email, string password);
    }
}
