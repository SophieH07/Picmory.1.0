using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Picmory.Models;
using Picmory.Models.Repositorys;
using Picmory.Models.RequestModels;
using Picmory.Models.RequestResultModels;
using Picmory.Util;
using System.Collections.Generic;
using System.IO;

namespace Picmory.Controllers
{
    [Authorize]
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

        
        [HttpGet("myinfo")]
        public UserPageUser Info()
        {
            if (userGet.HaveUser(HttpContext))
            {
                User user = userGet.GetUser(HttpContext);
                List<FolderForShow> foldersForShow = folderRepository.GetAllFolders(user);
                List<UserPageUser> followers = followerRepository.GetAllFollowers(user);
                UserPageUser resultUser = new UserPageUser(user.UserName, user.Email, user.ColorOne, user.ColorTwo, followers, 0, user.ProfilePicture, foldersForShow);
                return resultUser;
            }
            return null;
        }
       
        [HttpPost("chnagepassword")]
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
                else if (changeData.UserName == null)
                    { changeData.UserName = user.UserName; }
                else if (changeData.ColorOne == null)
                    { changeData.ColorOne = user.ColorOne; } 
                else if (changeData.ColorTwo == null)
                    { changeData.ColorTwo = user.ColorTwo; }
                user.UserName = changeData.UserName;
                user.ColorOne = (ThemeColor)changeData.ColorOne;
                user.ColorTwo = (ThemeColor)changeData.ColorTwo;
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
            if (userGet.HaveUser(HttpContext))
            {
                User user = userGet.GetUser(HttpContext);
                user.ProfilePicture = profilePicture;
                userRepository.EditUserData(user);
                return Ok();
            }
            return Unauthorized();
        }

    }
}
