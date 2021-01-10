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
        private readonly IFolderRepository folderRepository;
        public UserGet userGet { get; }

        public FolderController(IUserRepository userRepository, IFolderRepository folderRepository)
        {
            this.folderRepository = folderRepository;
            userGet = new UserGet(userRepository);
        }



        [HttpPost("createnewfolder")]
        public IActionResult CreateNewFolder([FromBody] Folder folder)
        {
            if (userGet.HaveUser(HttpContext))
            {
                Folder newFolder = new Folder(folder.FolderName, folder.Access, userGet.GetUser(HttpContext));
                folderRepository.SaveNewFolder(newFolder);
                return Ok();
            }
            return Unauthorized();
        }

        [HttpPost("changefolderdata")]
        public IActionResult ChangeFolderName(ChangeFolderData changeFolderData)
        {
            if (userGet.HaveUser(HttpContext))
            {
                Success success = folderRepository.ChangeFolderData(
                                                userGet.GetUser(HttpContext),
                                                changeFolderData.originalFolder,
                                                changeFolderData.newName,
                                                changeFolderData.newAccessType);
                switch (success) {
                    case Success.Successfull:
                        return Ok();
                    case Success.FailedByNotExist:
                        return BadRequest("Folder doesn't exist!");
                    case Success.FailedByUsedName:
                        return BadRequest("Foldername already used!");
                }     
            }
            return Unauthorized();
        }
    
        [HttpPost("deletefolder")]
        public IActionResult DeleteFolder([FromBody]string folderName)
        {
            if (userGet.HaveUser(HttpContext))
            {
                bool success = folderRepository.DeleteFolder(userGet.GetUser(HttpContext), folderName);
                if (success) { return Ok(); }
                else { return BadRequest("Not existing folder!"); }
            }
            return Unauthorized();
        }
    }
}
