using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Picmory.Models;
using Picmory.Models.Repositorys;
using Picmory.Models.RequestModels;
using Picmory.Models.RequestResultModels;
using Picmory.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Picmory.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IFolderRepository folderRepository;
        private IConfiguration _config;
        public UserController(IUserRepository userRepository, IFolderRepository folderRepository, IConfiguration config)
        {
            this.userRepository = userRepository;
            this.folderRepository = folderRepository;
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
                List<Folder> folders = folderRepository.GetAllFolders(user);
                List<FolderForShow> foldersForShow = new List<FolderForShow>();
                foreach (Folder folder in folders)
                {
                    foldersForShow.Add(new FolderForShow(folder.FolderName, folder.Access, user.UserName));
                }
                UserPageUser resultUser = new UserPageUser(user.UserName, user.Email, user.ColorOne, user.ColorTwo, 0, 0, 0, foldersForShow);
                result = JsonConvert.SerializeObject(resultUser);
            }
            return result;
        }
       
        [HttpPost("chnagepassword")]
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

        [HttpPost("changethemeandusername")]
        public IActionResult SetNewTheme([FromBody] ChangeUserData changeData)
        {
            IActionResult response = Unauthorized();
            var currentUser = HttpContext.User;
            if (currentUser.HasClaim(c => c.Type == "Id") && ModelState.IsValid)
            {
                int id = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                User user = userRepository.GetUserData(id);
                user.ColorOne = (ThemeColor)changeData.ColorOne;
                user.ColorTwo = (ThemeColor)changeData.ColorTwo;
                user.UserName = changeData.UserName;
                userRepository.EditUserData(user);
                response = Ok();                
            }
            return response;
        }
    }
}
