using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Picmory.Models;
using Picmory.Models.Repositorys;
using Picmory.Models.RequestModels;
using Picmory.Util;
using System.Collections.Generic;

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

        [HttpGet("getrequestedfollow")]
        public List<string> GetFollowingRequest()
        {
            if (userGet.HaveUser(HttpContext))
            {
                User user = userGet.GetUser(HttpContext);
                return followerRepository.GetAllFollowRequest(user);
            }
            return null;
        }

        [HttpPost("answerfollower")]
        public IActionResult AnswerFollower([FromBody] FollowerAccept followeraccept)
        {
            if (userGet.HaveUser(HttpContext))
            {
                User followedUser = userGet.GetUser(HttpContext);
                User followerUser = userRepository.GetUserData(followeraccept.UserName);
                followerRepository.AnswerNewFollower(followeraccept.Accept,followerUser,followedUser);
                return Ok();
            }
            return Unauthorized();
        }

        [HttpPost("deletefollower")]
        public IActionResult DeleteFollower([FromBody] FollowerAccept followeraccept)
        {
            if (userGet.HaveUser(HttpContext))
            {
                User followedUser = userGet.GetUser(HttpContext);
                User followerUser = userRepository.GetUserData(followeraccept.UserName);
                followerRepository.DeleteFollower(followerUser, followedUser);
                return Ok();
            }
            return Unauthorized();
        }
    }
}
