using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Picmory.Models;
using Picmory.Models.Repositorys;
using Picmory.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FollowerController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IFollowerRepository followerRepository;
        private readonly UserGet userGet;

        public FollowerController(IUserRepository userRepository, IFollowerRepository followerRepository)
        {
            this.userRepository = userRepository;
            this.followerRepository = followerRepository;
            userGet = new UserGet(userRepository);
        }

        [HttpPost("asknewfollower")]
        public IActionResult AskNewFollower([FromBody] string followerName)
        {
            if (userGet.HaveUser(HttpContext))
            {
                User user = userGet.GetUser(HttpContext);
                User followedUser = userRepository.GetUserData(followerName);
                followerRepository.AskNewFollower(user, followedUser);
                return Ok();
            }
            return Unauthorized();
        }
    }
}
