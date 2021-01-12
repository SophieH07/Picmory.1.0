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
                if (followedUser == null) { return BadRequest("Not exists user with username!"); }
                Success success = followerRepository.AskNewFollower(user, followedUser);
                switch (success)
                {
                    case (Success.Successfull):
                        return Ok();
                    case (Success.FailedByAlreadyRequested):
                        return BadRequest("Already requested!");
                    case (Success.FailedByAlreadyFollowed):
                        return BadRequest("Already followed!");
                }
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
                if (followerUser == null) { return BadRequest("Not exists user with username!"); }
                Success success = followerRepository.AnswerNewFollower(followeraccept.Accept,followerUser,followedUser);
                switch (success)
                {
                    case (Success.Successfull):
                        return Ok();
                    case (Success.FailedByAlreadyAnswered):
                        return BadRequest("Already answered!");
                    case (Success.FailedByNotRequested):
                        return BadRequest("Following not requested!");
                }
            }
            return Unauthorized();
        }

        [HttpPost("deletefollower")]
        public IActionResult DeleteFollower([FromBody] string userName)
        {
            if (userGet.HaveUser(HttpContext))
            {
                User followedUser = userGet.GetUser(HttpContext);
                User followerUser = userRepository.GetUserData(userName);
                if (followerUser == null) { return BadRequest("Not exists user with username!"); }
                Success success = followerRepository.DeleteFollower(followerUser, followedUser);
                switch (success)
                {
                    case (Success.Successfull):
                        return Ok();
                    case (Success.FailedByNotAccepted):
                        return BadRequest("Not answered!");
                    case (Success.FailedByNotExist):
                        return BadRequest("Following not requested!");
                }
            }
            return Unauthorized();
        }
    }
}
