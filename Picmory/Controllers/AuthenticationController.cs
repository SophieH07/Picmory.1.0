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


        [HttpPost("register")]
        public IActionResult Create([FromBody]User user)
        {
            return (!userRepository.UserNameAlreadyUsed(user.UserName) &&
                    user.Email != null &&
                    !userRepository.EmailAlreadyUsed(user.Email)) ?
                SaveUser(user) : BadRequest("Wrong Data!");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            string loginPassword = user.Password;
            User databaseUser = (user.Email == null) ? userRepository.GetUserData(user.UserName): userRepository.GetUserDataFromEmail(user.Email);
            if (databaseUser != null) {
                string originalPassword = databaseUser.Password;
                if (Hashing.ValidatePassword(loginPassword, originalPassword))
                {
                   return Ok(new { token = GenerateJSONWebToken(databaseUser) });
                }
                else { return  BadRequest("Wrong Password!"); }
            }
            else { return BadRequest("Wrong Username!"); }
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


        private IActionResult SaveUser(User user)
        {
            user.Password = Hashing.HashPassword(user.Password);
            User newUser = userRepository.RegisterNewUser(user);
            var tokenString = GenerateJSONWebToken(newUser);
            return Ok(new { token = tokenString });
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
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
