using AutoMapper;
using MarketplaceBackend.Common.Mappings;
using MarketplaceBackend.Models;

namespace MarketplaceBackend.Contracts.V1.Responses.Identity
{
    public class RefreshTokenResponse : IMapFrom<RefreshTokenResult>
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}
