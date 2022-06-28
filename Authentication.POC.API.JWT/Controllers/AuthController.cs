using Authentication.POC.API.JWT.Contracts;
using Authentication.POC.API.JWT.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authentication.POC.API.JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public record LoginModel([Required] string? EmailAddress, [Required] string? Password);
        public record AuthenticatedResponse(string? Token);

        private readonly IUserRepository _userRepository;
        private readonly IConfigurationSection _jwtSettings;

        public AuthController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _jwtSettings = configuration.GetSection("JWTSettings");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            if (login is null || string.IsNullOrWhiteSpace(login.EmailAddress))
            {
                return BadRequest("Invalid client request");
            }

            var loggedUser = await _userRepository.Login(login.EmailAddress, login.Password);
            if (loggedUser is null)
                return Unauthorized();

            var signinCredentials = GetSigninCredentials();
            var claims = GetClaims(loggedUser);
            var tokenOptions = GenerateTokenOptions(signinCredentials, claims);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new AuthenticatedResponse(tokenString));
        }

        private SymmetricSecurityKey GetSecretKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings["securityKey"]));

        private SigningCredentials GetSigninCredentials() =>
            new SigningCredentials(GetSecretKey(), SecurityAlgorithms.HmacSha256);

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signinCredentials, List<Claim> claims) =>
            new JwtSecurityToken(
                issuer: _jwtSettings["validIssuer"],
                audience: _jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(_jwtSettings["expiryInMinutes"])),
                signingCredentials: signinCredentials
            );

        private List<Claim> GetClaims(User user) =>
            new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role?.Name ?? string.Empty)
            };
    }
}
