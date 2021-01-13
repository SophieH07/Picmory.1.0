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
    
        public AuthenticationController(IUserRepository userRepository, IConfiguration config)
        {
            this.userRepository = userRepository;
            _config = config;
        }


        [Produces("application/json")]
        [HttpPost("register")]
        public IActionResult Create([FromBody]User user)
        {
            if (user.Email != null && user.UserName != null &&
                !userRepository.UserNameAlreadyUsed(user.UserName) &&
                !userRepository.EmailAlreadyUsed(user.Email))
            {
                User databaseUser = SaveUser(user);
                Response.Cookies.Append("Bearer", GenerateJSONWebToken(databaseUser), new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                return Ok();
            }
            return BadRequest("Wrong Data!");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUser user)
        {
            string loginPassword = user.Password;
            User databaseUser = userRepository.UserNameAlreadyUsed(user.UserName) ? userRepository.GetUserData(user.UserName) : userRepository.GetUserDataFromEmail(user.UserName);
            if (databaseUser != null) {
                string originalPassword = databaseUser.Password;
                if (Hashing.ValidatePassword(loginPassword, originalPassword))
                {
                    NavBarUser userData = new NavBarUser(databaseUser.UserName, databaseUser.ProfilePicture, databaseUser.ColorOne, databaseUser.ColorTwo);
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
