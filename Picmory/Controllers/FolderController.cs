using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Picmory.Models;
using Picmory.Models.Repositorys;
using Picmory.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Picmory.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FolderController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IFolderRepository folderRepository;
        private IConfiguration _config;
        public FolderController(IUserRepository userRepository, IFolderRepository folderRepository, IConfiguration config)
        {
            this.userRepository = userRepository;
            this.folderRepository = folderRepository;
            _config = config;
        }



        [HttpPost("createnewfolder")]
        public IActionResult CreateNewFolder([FromBody] Folder folder)
        {
            IActionResult response = Unauthorized();
            var currentUser = HttpContext.User;
            if (currentUser.HasClaim(c => c.Type == "Id"))
            {
                int id = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                User user = userRepository.GetUserData(id);
                Folder newFolder = new Folder(folder.FolderName, folder.Access, user);
                folderRepository.SaveNewFolder(newFolder);
                response = Ok();
            }
            return response;
        }
    }
}
