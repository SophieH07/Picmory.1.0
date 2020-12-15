using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Picmory.Models;
using Picmory.Models.Repositorys;
using Picmory.Models.RequestResultModels;
using Picmory.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PictureController :ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IPictureRepository pictureRepository;
        private IConfiguration _config;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PictureController(IUserRepository userRepository, IPictureRepository pictureRepository, IConfiguration config, IWebHostEnvironment hostEnvironment)
        {
            this.userRepository = userRepository;
            this.pictureRepository = pictureRepository;
            _config = config;
            this._hostEnvironment = hostEnvironment;
        }


        [HttpGet("{pictureId}")]
        public IActionResult GetImageById(string pictureId)
        {
            var currentUser = HttpContext.User;
            if (currentUser.HasClaim(c => c.Type == "Id"))
            {
                int id = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                User user = userRepository.GetUserData(id);
                Byte[] picture;
                string path = Path.Combine(_hostEnvironment.WebRootPath, user.UserName, pictureId);
                picture = System.IO.File.ReadAllBytes(path + ".png");
                return File(picture, "image/jpeg");
            }
            return Unauthorized();
        }

        [HttpPost("uploadPicture")]
        public IActionResult SetNewPassword()
        {
            var request = HttpContext.Request;
            UploadPhoto photo = new UploadPhoto(request.Form["Description"].ToString(), request.Form["Access"].ToString(), request.Form["FolderName"].ToString()) ;
            IActionResult response = Unauthorized();
            var currentUser = HttpContext.User;
            if (currentUser.HasClaim(c => c.Type == "Id") && ModelState.IsValid)
            {
                int id = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                User user = userRepository.GetUserData(id);
                IFormFile image = (IFormFile)request.Form.Files[0];
                if (image != null) {
                    string uploadFolder = Path.Combine(_hostEnvironment.WebRootPath, user.UserName);
                    Picture picture = new Picture(photo.Description, photo.Access, image.ContentType, user, photo.FolderName);
                    Picture savedPicture = pictureRepository.SavePicture(picture);
                    string filePath = Path.Combine(uploadFolder, savedPicture.Id.ToString());
                    bool saved = pictureRepository.SavePicturePath(user, savedPicture.Id, filePath);
                    string fullFilePath = filePath + "." + image.ContentType.ToString().Substring(6);
                    image.CopyTo(new FileStream(fullFilePath, FileMode.Create));
                    response = Ok();
                }
            }
            return response;
        }
    }
}
