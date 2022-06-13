using MarketplaceBackend.Models;

namespace MarketplaceBackend.Services
{
    public class IdentityService : IIdentityService
    {
        public Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticationResult> RegisterAsync(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
