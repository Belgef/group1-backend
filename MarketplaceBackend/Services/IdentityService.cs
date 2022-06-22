using MarketplaceBackend.Contracts.V1.Requests.Identity;
using MarketplaceBackend.Data;
using MarketplaceBackend.Helpers;
using MarketplaceBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MarketplaceBackend.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly AuthSettings _authSettings;
        private readonly DataContext _context;

        public IdentityService(DataContext context, AuthSettings authSettings)
        {
            _authSettings = authSettings;
            _context = context;
        }

        public async Task<AuthenticationResult> RegisterAsync(UserRegistrationRequest user)
        {
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User with this email address already exists" }
                };
            }

            var salt = PasswordEncoder.GenerateSalt();

            var createdUser = new User { 
                FirstName = user.FirstName,
                LastName = user.LastName, 
                Email = user.Email, 
                Hash = PasswordEncoder.HashPassword(user.Password, salt), 
                Salt = Convert.ToBase64String(salt), 
                Role = Role.User };

            var token = GenerateToken(createdUser);

            var refreshToken = GenerateRefreshToken();

            createdUser.RefreshToken = refreshToken;
            createdUser.RefreshTokenExpiryTime = DateTime.UtcNow.Add(_authSettings.RefreshTokenLifetime);

            _context.Users.Add(createdUser);
            await _context.SaveChangesAsync();

            return new AuthenticationResult
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                TokenExpiryTime = token.ValidTo,
                Success = true,
                User = createdUser
            };
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist" }
                };
            }

            if(!PasswordEncoder.ValidatePassword(password, user.Hash, user.Salt))
                return new AuthenticationResult
                {
                    Errors = new[] { "User/password combination is wrong" }
                };

            var token = GenerateToken(user);

            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.Add(_authSettings.RefreshTokenLifetime);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new AuthenticationResult { 
                Token = new JwtSecurityTokenHandler().WriteToken(token), 
                TokenExpiryTime = token.ValidTo,
                Success = true, 
                User = user 
            };
        }

        private SecurityToken GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authSettings.Key);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                new Claim("id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name,user.Email),
                new Claim("fname", user.FirstName),
                new Claim("lname", user.LastName),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _authSettings.Issuer,
                Audience = _authSettings.Audience,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_authSettings.TokenLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return token;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<RefreshTokenResult> RefreshTokenAsync(string token, string refreshToken)
        {
            var principal = GetPrincipalFromExpiredToken(token);
            if (principal == null)
            {
                return new RefreshTokenResult
                {
                    Errors = new[] { "Invalid access token or refresh token" }
                };
            }

            string email = principal.Identity.Name;

            var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == email);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return new RefreshTokenResult
                {
                    Errors = new[] { "Invalid access token or refresh token" }
                };
            }

            var newToken = GenerateToken(user);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.Add(_authSettings.RefreshTokenLifetime);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new RefreshTokenResult
            {
                Success = true,
                Token = new JwtSecurityTokenHandler().WriteToken(newToken),
                RefreshToken = newRefreshToken
            };
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authSettings.Key)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}
