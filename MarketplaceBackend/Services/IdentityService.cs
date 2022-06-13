using MarketplaceBackend.Contracts.V1.Requests.Identity;
using MarketplaceBackend.Data;
using MarketplaceBackend.Helpers;
using MarketplaceBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

            _context.Users.Add(createdUser);
            await _context.SaveChangesAsync();

            return new AuthenticationResult { Token = token, Success = true, User = createdUser};
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

            return new AuthenticationResult { Token = token, Success = true, User = user };
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authSettings.Key);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                new Claim("id", user.Id.ToString()),
                new("fname", user.FirstName),
                new("lname", user.LastName),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
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

            return tokenHandler.WriteToken(token);
        }
    }
}
