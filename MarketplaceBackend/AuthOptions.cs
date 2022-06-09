using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MarketplaceBackend
{
    public class AuthOptions
    {
        public const string ISSUER = "MarketplaceEdPractServer";
        public const string AUDIENCE = "MarketplaceEdPractClient";
        const string KEY = "vtrsyrfsy-9-73-33";
        public const int LIFETIME = 60;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
            => new(Encoding.ASCII.GetBytes(KEY));
    }
}
