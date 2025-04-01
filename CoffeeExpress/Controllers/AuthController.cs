using CoffeeExpress.DTOs;
using CoffeeExpress.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoffeeExpress.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly CoffeeEpxpressDBContext _context;

        public AuthController(CoffeeEpxpressDBContext context)
        {
            _context = context;
        }
        //registro de usuario
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if (_context.Users.Any(u => u.Email == registerRequest.Email))
            {
                return BadRequest("The email is already in use.");
            }

            var role = await _context.UserRoles.FindAsync(1);
            if (role == null)
            {
                return BadRequest("Invalid role.");
            }

            User user = new User
            {
                Name = registerRequest.Name,
                Email = registerRequest.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password),
                IdUserRole = role.IdUserRole,
                UserRole = role,
                CreatedAt = DateTime.UtcNow,
                State = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User successfully registered.");
        }

        //login usuario
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == loginRequest.Email);
            var role = await _context.UserRoles.FindAsync(user.IdUserRole);

            if (role == null)
            {
                return BadRequest("Invalid user.");
            }

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
            {
                return Unauthorized("Incorrect credentails.");
            }

            user.UserRole = role;

            var token = generateJwtToken(user);
            return Ok(new { Token = token, User = new { user.IdUser, user.Name, user.Email, user.UserRole.UserRoleName } });
        } 

        private string generateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim("id", user.IdUser.ToString()),
                new Claim("role", user.UserRole.UserRoleName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("clave-muy-segura-para-el-token-123456"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "CoffeeExpressAPI",
                audience: "CoffeeExpressAPI",
                claims = claims,
                expires: DateTime.UtcNow.AddDays(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
