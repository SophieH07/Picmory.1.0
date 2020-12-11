using Microsoft.AspNetCore.Mvc;
using Picmory.Models;
using Picmory.Models.Repositorys;
using System;
using Picmory.Util;


namespace Picmory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost("registration")]
        public bool Create([FromBody]User user)
        {
            if (!userRepository.GetUserExist(user.Name))
            {
                user.Password = Hashing.HashPassword(user.Password);
                User newUser = userRepository.RegisterNewUser(user);
                return true;
            }
            return false;
        }

        [HttpPost("login")]
        public bool Login([FromBody] User user)
        {
            string loginPassword = user.Password;
            User databaseUser = userRepository.GetUserData(user.Name);
            if (databaseUser != null) {
                string originalPassword = databaseUser.Password;
                if (Hashing.ValidatePassword(loginPassword, originalPassword))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
