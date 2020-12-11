using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Picmory.Models.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Controllers
{
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

        [Authorize]
        [HttpGet("userinfo")]
        public string Info()
        {
           string result = "Not";
            var currentUser = HttpContext.User;
            if (currentUser.HasClaim(c => c.Type == "Id"))
            {
                result = currentUser.Claims.FirstOrDefault().Value;
            }
            return result;
        }

    }
}
