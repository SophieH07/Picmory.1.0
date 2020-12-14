﻿using Microsoft.AspNetCore.Mvc;
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


        [HttpPost("registration")]
        public IActionResult Create([FromBody]User user)
        {
            IActionResult response = Unauthorized();
            if (!userRepository.GetUserExist(user.UserName))
            {
                user.Password = Hashing.HashPassword(user.Password);
                User newUser = userRepository.RegisterNewUser(user);
                var tokenString = GenerateJSONWebToken(newUser);
                response = Ok(new { token = tokenString });
            }
            return response;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            IActionResult response = Unauthorized();
            string loginPassword = user.Password;
            User databaseUser = userRepository.GetUserData(user.UserName);
            if (databaseUser != null) {
                string originalPassword = databaseUser.Password;
                if (Hashing.ValidatePassword(loginPassword, originalPassword))
                {
                    var tokenString = GenerateJSONWebToken(databaseUser);
                    response = Ok(new { token = tokenString });
                }
            }
            return response;
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