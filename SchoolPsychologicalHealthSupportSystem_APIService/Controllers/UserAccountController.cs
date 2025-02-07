using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SchoolPsychologicalHealthSupportSystem.Models;
using SchoolPsychologicalHealthSupportSystem_Service;

namespace SchoolPsychologicalHealthSupportSystem_APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserAccountService _userAccountService;

        public UserAccountController(IConfiguration configuration, IUserAccountService userAccountService)
        {
            _configuration = configuration;
            _userAccountService = userAccountService;
        }

        // POST: api/UserAccount/Login
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _userAccountService.Authenticate(request.userName, request.password);

            if (user == null || user.Result == null)
                return Unauthorized();

            var token = GenerateJSONWebToken(user.Result);

            return Ok(token);
        }

        private string GenerateJSONWebToken(UserAccount user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"]
                    , _configuration["Jwt:Audience"]
                    , new Claim[]
                    {
                new(ClaimTypes.Name, user.UserName),
                //new(ClaimTypes.Email, systemUserAccount.Email),
                new(ClaimTypes.Role, user.RoleId.ToString()),
                    },
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

        public sealed record LoginRequest(string userName, string password);
    }
}
