using MarketplaceBackend.Common.Mappings;
using MarketplaceBackend.Models;

namespace MarketplaceBackend.Contracts.V1.Responses.Identity
{
    public class AuthFailedResponse : IMapFrom<AuthenticationResult>
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
