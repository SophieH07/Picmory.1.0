using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Picmory.Models;
using Picmory.Models.Repositorys;
using Picmory.Models.RequestResultModels;
using Picmory.Util;
using System.Linq;

namespace Picmory.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private IConfiguration _config;

        public UserController(IUserRepository userRepository, IConfiguration config)
        {
            this.userRepository = userRepository;
            _config = config;
        }

        
        [HttpGet("userinfo")]
        public string Info()
        {
            string result = null;
            var currentUser = HttpContext.User;
            if (currentUser.HasClaim(c => c.Type == "Id"))
            {
                int id = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                User user = userRepository.GetUserData(id);
                UserPageUser resultUser = new UserPageUser(user.UserName, user.EMail, user.ColorOne, user.ColorTow, 0, 0, user.ProfilePicture);
                result = JsonConvert.SerializeObject(resultUser);
            }
            return result;
        }
       
        [HttpPost("setNewPassword")]
        public IActionResult SetNewPassword([FromBody] string newPassword)
        {
            IActionResult response = Unauthorized();
            var currentUser = HttpContext.User;
            if (currentUser.HasClaim(c => c.Type == "Id"))
            {
                int id = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                User user = userRepository.GetUserData(id);
                user.Password = Hashing.HashPassword(newPassword);
                userRepository.EditUserData(user);
                response = Ok(); 
            }
            return response;
        }
        }
}
