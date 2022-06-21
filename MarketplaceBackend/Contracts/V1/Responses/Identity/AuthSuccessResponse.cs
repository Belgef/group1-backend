using AutoMapper;
using MarketplaceBackend.Common.Mappings;
using MarketplaceBackend.Models;

namespace MarketplaceBackend.Contracts.V1.Responses.Identity
{
    public class AuthSuccessResponse : IMapFrom<AuthenticationResult>
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public DateTime TokenExpiryTime { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AvatarUrl { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AuthenticationResult, AuthSuccessResponse>()
                .ForMember(d => d.Token, opt => opt.MapFrom(s => s.Token))
                .ForMember(d => d.RefreshToken, opt => opt.MapFrom(s => s.User.RefreshToken))
                .ForMember(d => d.TokenExpiryTime, opt => opt.MapFrom(s => s.TokenExpiryTime))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.User.Email))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.User.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.User.LastName))
                .ForMember(d => d.AvatarUrl, opt => opt.MapFrom(s => s.User.AvatarUrl));
        }
    }
}
