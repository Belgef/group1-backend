using MarketplaceBackend.Contracts.V1.Requests.Identity;
using MarketplaceBackend.Models;

namespace MarketplaceBackend.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(UserRegistrationRequest user);
        Task<AuthenticationResult> LoginAsync(string email, string password);
    }
}
