using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Picmory.Models;
using Picmory.Models.Repositorys;
using Picmory.Models.RequestModels;
using Picmory.Models.RequestResultModels;
using Picmory.Util;
using System.Collections.Generic;

namespace Picmory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IFolderRepository folderRepository;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IPictureRepository pictureRepository;
        private readonly IFollowerRepository followerRepository;
        private readonly UserGet userGet;

        public UserController(IUserRepository userRepository, IFolderRepository folderRepository, IWebHostEnvironment hostEnvironment, IPictureRepository pictureRepository, IFollowerRepository followerRepository)
        {
            this.userRepository = userRepository;
            this.folderRepository = folderRepository;
            this.pictureRepository = pictureRepository;
            this.followerRepository = followerRepository;
            _hostEnvironment = hostEnvironment;
            userGet = new UserGet(userRepository);
        }

        [Produces("application/json")]
        [HttpGet("myinfo")]
        public IActionResult Info()
        {
            if (userGet.HaveUser(HttpContext))
            {
                User user = userGet.GetUser(HttpContext);
                List<FolderForShow> foldersForShow = folderRepository.GetAllFolders(user);
                List<string> followers = followerRepository.GetAllFollowers(user);
                List<string> following = followerRepository.GetAllFollowing(user);
                UserPageUser resultUser = new UserPageUser(user.UserName, user.Email, user.ColorOne, user.ColorTwo, followers, following, user.ProfilePicture, foldersForShow);
                return Ok(resultUser);
            }
            return Unauthorized();
        }
       
        [HttpPost("changepassword")]
        public IActionResult SetNewPassword([FromBody] string newPassword)
        {
            if (userGet.HaveUser(HttpContext))
            {
                if (newPassword != null) { 
                User user = userGet.GetUser(HttpContext);
                user.Password = Hashing.HashPassword(newPassword);
                userRepository.EditUserData(user);
                return Ok(); }
            }
            return Unauthorized();
        }

        [HttpPost("changethemeandusername")]
        public IActionResult SetNewData([FromBody] ChangeUserData changeData)
        {
            if (userGet.HaveUser(HttpContext))
            {
                User user = userGet.GetUser(HttpContext);

                if (userRepository.UserNameAlreadyUsed(changeData.UserName) && changeData.UserName != user.UserName)
                    { return BadRequest("Used Username!"); }
                else if (changeData.UserName != null)
                    { user.UserName = changeData.UserName; }
                else if (changeData.ColorOne != null)
                    { user.ColorOne = (ThemeColor)changeData.ColorOne; } 
                else if (changeData.ColorTwo != null)
                    { user.ColorTwo = (ThemeColor)changeData.ColorTwo; }
                userRepository.EditUserData(user);

                return Ok();                
            }
            return Unauthorized();
        }

        [HttpPost("setprofilepicture")]
        public IActionResult SetProfilePicture([FromBody] string profilePictureId)
        {
            
            int.TryParse(profilePictureId, out int pictureId);
            if (pictureId ==0) { return BadRequest("Wrong data!"); } 
            Picture profilePicture = pictureRepository.GetPicture(pictureId);
            if (profilePicture == null) { return BadRequest("Not your picture!"); }
            if (userGet.HaveUser(HttpContext))
            {
                User user = userGet.GetUser(HttpContext);
                if (profilePicture.Owner == user)
                {
                    user.ProfilePicture = profilePicture;
                    userRepository.EditUserData(user);
                    return Ok();
                }
                return BadRequest("Not your picture!");
            }
            return Unauthorized();
        }

        [HttpPost("deleteuser")]
        public IActionResult Deleteuser()
        {
            if (userGet.HaveUser(HttpContext))
            {
                User userToDelete = userGet.GetUser(HttpContext);
                userRepository.DeleteUser(userToDelete);
                return Ok();
            }
            return Unauthorized();
        }
    }
}
