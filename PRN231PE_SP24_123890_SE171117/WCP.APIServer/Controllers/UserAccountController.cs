using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WCP.Repositories.Models;
using WCP.Services;

namespace WCP.APIServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserAccountService _userAccountService;
        public UserAccountController(IConfiguration config, IUserAccountService userAccountService)
        {
            _config = config;
            _userAccountService = userAccountService;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginReqeust request)
        {
            var user = _userAccountService.Authenticate(request.UserEmail, request.UserPassword);

            if (user == null || user.Result == null)
                return Unauthorized();

            var token = GenerateJSONWebToken(user.Result);

            return Ok(token);
        }

        private string GenerateJSONWebToken(UserAccount userAccount)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"]
                    , _config["Jwt:Audience"]
                    , new Claim[]
                    {
                new(ClaimTypes.Name, userAccount.UserFullName),
                new(ClaimTypes.Role, userAccount.Role.ToString()),
                    },
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

        public sealed record LoginReqeust(string UserEmail, string UserPassword);
    }
}
