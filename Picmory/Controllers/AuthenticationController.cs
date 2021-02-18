using Microsoft.AspNetCore.Mvc;
using Picmory.Models;
using Picmory.Models.Repositorys;
using System;
using Picmory.Util;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Picmory.Models.RequestModels;
using Microsoft.AspNetCore.Http;

namespace Picmory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private IConfiguration _config;
        private readonly UserGet userGet;

        public AuthenticationController(IUserRepository userRepository, IConfiguration config)
        {
            this.userRepository = userRepository;
            _config = config;
            userGet = new UserGet(userRepository);
        }


        [HttpPost("register")]
        public IActionResult Create([FromBody]User user)
        {
            if (!userRepository.UserNameAlreadyUsed(user.UserName) &&
                !userRepository.EmailAlreadyUsed(user.Email))
            {
                User databaseUser = SaveUser(user);
                Response.Cookies.Append("Bearer", GenerateJSONWebToken(databaseUser), new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                return Ok();
            }
            return BadRequest("Wrong Data!");
        }

        [HttpPost("login")]
        [Produces("application/json")]
        public IActionResult Login([FromBody] LoginUser user)
        {
            User databaseUser = userRepository.UserNameAlreadyUsed(user.UserName) 
                ? userRepository.GetUserData(user.UserName) 
                : userRepository.GetUserDataFromEmail(user.UserName);
            if (databaseUser != null) {
                if (Hashing.ValidatePassword(user.Password, databaseUser.Password))
                {
                    NavBarUser userData = new NavBarUser(databaseUser);
                    Response.Cookies.Append("Bearer", GenerateJSONWebToken(databaseUser), new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                    return Ok(userData);
                }
                else { return  BadRequest("Wrong Password!"); }
            }
            else { return BadRequest("Wrong Username or E-mail!"); }
        }

        [HttpPost("checkusernamealreadyexist")]
        public bool CheckUsername([FromBody] string username)  
        {
            return userRepository.UserNameAlreadyUsed(username);
        }

        [HttpPost("checkemailalreadyexist")]
        public bool CheckEmail([FromBody] string email)
        {
            return userRepository.EmailAlreadyUsed(email);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            if (userGet.HaveUser(HttpContext))
            {
                Response.Cookies.Delete("Bearer");
                return Ok();
                
            }
            return Unauthorized();
        }


        private User SaveUser(User user)
        {
            user.Password = Hashing.HashPassword(user.Password);
            User newUser = userRepository.RegisterNewUser(user);
            return newUser;
        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
            new Claim("Id", userInfo.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddDays(365),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
