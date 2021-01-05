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
        private readonly UserGet userGet;

        public UserController(IUserRepository userRepository, IFolderRepository folderRepository, IWebHostEnvironment hostEnvironment, IPictureRepository pictureRepository)
        {
            this.userRepository = userRepository;
            this.folderRepository = folderRepository;
            this.pictureRepository = pictureRepository;
            _hostEnvironment = hostEnvironment;
            userGet = new UserGet(userRepository);
        }

        
        [HttpGet("userinfo")]
        public string Info()
        {
            string result = null;
            if (userGet.HaveUser(HttpContext))
            {
                User user = userGet.GetUser(HttpContext);
                List<FolderForShow> foldersForShow = folderRepository.GetAllFolders(user);
                UserPageUser resultUser = new UserPageUser(user.UserName, user.Email, user.ColorOne, user.ColorTwo, 0, 0, user.ProfilePicture, foldersForShow);
                result = JsonConvert.SerializeObject(resultUser);
            }
            return result;
        }
       
        [HttpPost("chnagepassword")]
        public IActionResult SetNewPassword([FromBody] string newPassword)
        {
            IActionResult response = Unauthorized();
            if (userGet.HaveUser(HttpContext))
            {
                User user = userGet.GetUser(HttpContext);
                user.Password = Hashing.HashPassword(newPassword);
                userRepository.EditUserData(user);
                response = Ok(); 
            }
            return response;
        }

        [HttpPost("changethemeandusername")]
        public IActionResult SetNewData([FromBody] ChangeUserData changeData)
        {
            IActionResult response = Unauthorized();
            if (userGet.HaveUser(HttpContext))
            {
                User user = userGet.GetUser(HttpContext);
                if (userRepository.UserNameAlreadyUsed(changeData.UserName) && user.UserName != changeData.UserName )
                    { return BadRequest("Used Username!"); }
                user.UserName = changeData.UserName;
                user.ColorOne = (ThemeColor)changeData.ColorOne;
                user.ColorTwo = (ThemeColor)changeData.ColorTwo;
                userRepository.EditUserData(user);

                response = Ok();                
            }
            return response;
        }

        [HttpPost("setprofilepicture")]
        public IActionResult SetProfilePicture([FromBody] string profilePictureId)
        {
            IActionResult response = Unauthorized();
            Picture profilePicture = pictureRepository.GetPicture(int.Parse(profilePictureId));
            if (userGet.HaveUser(HttpContext) && profilePicture != null)
            {
                User user = userGet.GetUser(HttpContext);
                user.ProfilePicture = profilePicture;
                userRepository.EditUserData(user);
                response = Ok();
            }
            return response;
        }

        private void ChangeFolderName(string originalUsername, string newUserName)
        {
            string originalDirectoryPath = Path.Combine(_hostEnvironment.WebRootPath, originalUsername);
            string newDirectoryPath = Path.Combine(_hostEnvironment.WebRootPath, newUserName);
            if (Directory.Exists(originalDirectoryPath))
            {
                Directory.Move(originalDirectoryPath, newDirectoryPath);
            }
        }

    }
}
