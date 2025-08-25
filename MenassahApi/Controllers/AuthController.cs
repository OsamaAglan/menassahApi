using Menassah.Repository;
using Menassah.Shared;
using MenassahApi.DL;
using MenassahApi.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Menassah

{
    //[AuthorizeToken]
    // test
    // test2
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        //public IActionResult Login(UsersDL UsersDL ,[FromBody] LoginRequest request)
        //{
        //    if (request.UserName == UsersDL.UserName && request.Password == UsersDL.PasswordHash)
        //    {
        //        var roles = new List<string> { "Teacher", "Student" };

        //        var token = GenerateJwtToken(request.UserName, roles);
        //        return Ok(new LoginResponse
        //        {
        //            AccessToken = token,
        //            Expiration = DateTime.UtcNow.AddMinutes(120),
        //            UserName = request.UserName,
        //            Roles = roles
        //        });
        //    }

        //    return Unauthorized("اسم المستخدم أو كلمة المرور غير صحيحة");
        //}

        public IActionResult Login([FromBody] LoginRequest request)
        {
            // 👇 اختبار البيانات بشكل وهمي (استبدلها لاحقاً بقاعدة البيانات)
            if (request.UserName == "osama" && request.Password == "1234")
            {
                var roles = new List<string> { "Teacher", "Student" };

                var token = GenerateJwtToken(request.UserName, roles);
                return Ok(new LoginResponse
                {
                    AccessToken = token,
                    Expiration = DateTime.UtcNow.AddMinutes(120),
                    UserName = request.UserName,
                    Roles = roles
                });
            }

            return Unauthorized("اسم المستخدم أو كلمة المرور غير صحيحة");
        }

        private string GenerateJwtToken(string username, List<string> roles)
        {
            var jwtSettings = _config.GetSection("JwtSettings");

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }


}