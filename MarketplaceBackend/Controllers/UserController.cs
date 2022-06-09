using MarketplaceBackend.Data;
using MarketplaceBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MarketplaceBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public UserController(DataContext dataContext, IConfiguration configuration)
        {
            _context = dataContext;
            _config = configuration;
        }

        [HttpPost("/token")]
        public IActionResult Token([FromForm] string email, [FromForm] string password)
        {
            var identity = GetIdentity(email, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid email or password." });
            }

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: _config.GetValue<string>("Jwt:Issuer"),
                    audience: _config.GetValue<string>("Jwt:Audience"),
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromSeconds(_config.GetValue<int>("Jwt:Lifetime"))),
                    signingCredentials: new(
                            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.GetValue<string>("Jwt:Key"))),
                            SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };
            return Ok(response);
        }

        private ClaimsIdentity GetIdentity(string email, string password)
        {
            User user = _context.Users.Include(e => e.Role).FirstOrDefault(x => x.Email == email && x.Password == password);
            if (user != null)
            {
                List<Claim> claims = new(){
                    new("user_id", user.Id.ToString()),
                    new(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
                };
                ClaimsIdentity claimsIdentity = new(claims, "Token",
                    ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }
    }
}
