using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Picmory.Models;
using Picmory.Models.Repositorys;
using Picmory.Models.RequestModels;
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

        [HttpPost("changefolderdata")]
        public IActionResult ChangeFolderName(ChangeFolderData changeFolderData)
        {
            IActionResult response = Unauthorized();
            var currentUser = HttpContext.User;
            if (currentUser.HasClaim(c => c.Type == "Id"))
            {
                int id = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                User user = userRepository.GetUserData(id);
                folderRepository.ChangeFolderData(user,changeFolderData.originalFolder, changeFolderData.newName, changeFolderData.newAccessType);
                response = Ok();
            }
            return response;
        }
    
        [HttpPost("deletefolder")]
        public IActionResult DeleteFolder(string folderName)
        {
            IActionResult response = Unauthorized();
            var currentUser = HttpContext.User;
            if (currentUser.HasClaim(c => c.Type == "Id"))
            {
                int id = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                User user = userRepository.GetUserData(id);
                folderRepository.DeleteFolder(user, folderName);
                response = Ok();
            }
            return response;
        }
    }
}
