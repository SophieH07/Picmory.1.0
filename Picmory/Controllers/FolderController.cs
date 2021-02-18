using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Picmory.Models;
using Picmory.Models.Repositorys;
using Picmory.Models.RequestModels;
using Picmory.Util;
using System.Linq;

namespace Picmory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FolderController : ControllerBase
    {
        private readonly IFolderRepository folderRepository;
        public UserGet userGet { get; }
        private readonly PictureRemover pictureRemover;

        public FolderController(IUserRepository userRepository, IFolderRepository folderRepository, IWebHostEnvironment hostEnvironment, IPictureRepository pictureRepository)
        {
            this.folderRepository = folderRepository;
            userGet = new UserGet(userRepository);
            pictureRemover = new PictureRemover(hostEnvironment, pictureRepository);
        }



        [HttpPost("createnewfolder")]
        public IActionResult CreateNewFolder([FromBody] ChangeFolderData folder)
        {
            if (userGet.HaveUser(HttpContext) )
            {
                if (folder.Access != null && folder.Name != null) 
                { 
                    Folder newFolder = new Folder(folder.Name, (AccessType)folder.Access, userGet.GetUser(HttpContext));
                    Success success = folderRepository.SaveNewFolder(newFolder);
                    switch (success)
                    {
                        case Success.Successfull:
                            return Ok();
                        case Success.FailedByUsedName:
                            return BadRequest("Foldername already used!");
                    }
                }
                return BadRequest("Not valid AccessType, or FolderName!");
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
                                                changeFolderData.Name,
                                                changeFolderData.Access);
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

                pictureRemover.DeletePicturesForFolder(userGet.GetUser(HttpContext), folderName);
                Success success = folderRepository.DeleteFolder(userGet.GetUser(HttpContext), folderName);
                switch (success)
                {
                    case Success.Successfull:
                        return Ok();
                    case Success.FailedByNotExist:
                        return BadRequest("Folder doesn't exist!");
                }
            }
            return Unauthorized();
        }
    }
}
