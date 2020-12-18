using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Picmory.Models;
using Picmory.Models.Repositorys;
using Picmory.Models.RequestModels;
using Picmory.Util;
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
        public UserGet userGet { get; }

        public FolderController(IUserRepository userRepository, IFolderRepository folderRepository)
        {
            this.userRepository = userRepository;
            this.folderRepository = folderRepository;
            userGet = new UserGet(userRepository);
        }



        [HttpPost("createnewfolder")]
        public IActionResult CreateNewFolder([FromBody] Folder folder)
        {
            IActionResult response = Unauthorized();
            if (userGet.HaveUser(HttpContext))
            {
                Folder newFolder = new Folder(folder.FolderName, folder.Access, userGet.GetUser(HttpContext));
                folderRepository.SaveNewFolder(newFolder);
                response = Ok();
            }
            return response;
        }

        [HttpPost("changefolderdata")]
        public IActionResult ChangeFolderName(ChangeFolderData changeFolderData)
        {
            IActionResult response = Unauthorized();
            if (userGet.HaveUser(HttpContext))
            {
                folderRepository.ChangeFolderData(
                                                userGet.GetUser(HttpContext),
                                                changeFolderData.originalFolder,
                                                changeFolderData.newName,
                                                changeFolderData.newAccessType);
                response = Ok();
            }
            return response;
        }
    
        [HttpPost("deletefolder")]
        public IActionResult DeleteFolder(string folderName)
        {
            IActionResult response = Unauthorized();
            if (userGet.HaveUser(HttpContext))
            {
                folderRepository.DeleteFolder(userGet.GetUser(HttpContext), folderName);
                response = Ok();
            }
            return response;
        }
    }
}
