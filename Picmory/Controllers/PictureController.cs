using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Picmory.Models;
using Picmory.Models.Repositorys;
using Picmory.Models.RequestModels;
using Picmory.Models.RequestResultModels;
using Picmory.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Picmory.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PictureController :ControllerBase
    {
        private readonly string[] AcceptedFileTypes = { "image/gif", "image/png", "image/jpg", "image/jpeg" };
        private readonly IPictureRepository pictureRepository;
        private readonly IFolderRepository folderRepository;
        private readonly UserGet userGet;
        private readonly IWebHostEnvironment _hostEnv;

        public int ResponsePictures { get; private set; }

        public PictureController(IUserRepository userRepository, IPictureRepository pictureRepository, IWebHostEnvironment hostEnvironment, IFolderRepository folderRepository)
        {
            this.pictureRepository = pictureRepository;
            _hostEnv = hostEnvironment;
            this.folderRepository = folderRepository;
            userGet = new UserGet(userRepository);
        }


        [HttpGet("{pictureid}")]
        public IActionResult GetImageById(string pictureId)
        {
            string pictureType = pictureRepository.GetPictureType(int.Parse(pictureId));
            if (userGet.HaveUser(HttpContext) && pictureType != null)
            {
                string path = CreatePathForRetrive(pictureId, pictureType);
                Byte[] picture = System.IO.File.ReadAllBytes(path);
                return File(picture, pictureType);
            }
            return Unauthorized();
        }

        [HttpPost("uploadpicture")]
        public IActionResult UploadPicture()
        {
            IFormFile uploadedImage = HttpContext.Request.Form.Files[0];
            Folder folder = folderRepository.GetFolder(userGet.GetUser(HttpContext), HttpContext.Request.Form["FolderName"].ToString());
            UploadPhoto photoData = new UploadPhoto(
                                                HttpContext.Request.Form["Description"].ToString(),
                                                HttpContext.Request.Form["Access"].ToString(),
                                                folder);
            if (userGet.HaveUser(HttpContext) && ModelState.IsValid && uploadedImage != null && ImageTypeIsValid(uploadedImage))
            {
                UploadImage(HttpContext, photoData, uploadedImage);
                return Ok();
            }
            return Unauthorized();
        }

        [HttpPost("editpicture")]
        public IActionResult EditPicture([FromBody] PictureChange changeData)
        {
            if (userGet.HaveUser(HttpContext))
            {
                User user = userGet.GetUser(HttpContext);
                Picture picture = pictureRepository.GetPicture(changeData.Id);
                if (picture == null) { return BadRequest("Not existing picture!"); }
                if (picture.Owner == user)
                {
                    changeData.Owner = user;
                    Success success = pictureRepository.ChangePictureData(changeData);
                    switch (success)
                    {
                        case Success.Successfull:
                            return Ok();
                        case Success.FailedByNotExistFolderName:
                            return BadRequest("Folder doesn't exist!");
                        
                    }
                }
                return BadRequest("Not your picture!");
            }
            return Unauthorized();
        }

        [HttpPost("deletepicture")]
        public IActionResult DeletePicture([FromBody] string id)
        {
            if (userGet.HaveUser(HttpContext))
            {
                User user = userGet.GetUser(HttpContext);
                int.TryParse(id, out int pictureId);
                Picture picture = pictureRepository.GetPicture(pictureId);
                if (picture == null) { return BadRequest("Not existing picture!"); }
                if (picture.Owner == user)
                {
                    Success success = pictureRepository.DeletePicture(pictureId);
                    if (success == Success.Successfull) 
                    {
                        System.IO.File.Delete(_hostEnv.WebRootPath + "/" + pictureId.ToString());
                        return Ok();
                    }
                }
                return BadRequest("Not your picture!");
            }
            return Unauthorized();
        }

        [HttpGet("getmyimages")]
        public string GetImageForMe(PictureRequest data)
        {
            if (userGet.HaveUser(HttpContext))
            {
                List<ResponsePicture> responsePictures= new List<ResponsePicture>();
                List<Picture> pictures =  pictureRepository.GetPicturesForMe(userGet.GetUser(HttpContext), data.Offset, data.FolderName);
                foreach (Picture picture in pictures)
                {
                    responsePictures.Add(new ResponsePicture(picture.Id, picture.Description, picture.Folder.FolderName, picture.Access, picture.UploadDate));
                }
                return JsonConvert.SerializeObject(responsePictures);
            }
            return null;
        }


        private bool ImageTypeIsValid(IFormFile uploadedImage)
        {
            return (AcceptedFileTypes.Contains(uploadedImage.ContentType));
        }

        private void UploadImage(HttpContext context, UploadPhoto photoData, IFormFile uploadedImage)
        {
           
            Picture imageDataForDB = new Picture(
                                            photoData.Description,
                                            photoData.Access,
                                            uploadedImage.ContentType,
                                            userGet.GetUser(context),
                                            photoData.Folder);
            Picture savedImageData = pictureRepository.SavePicture(imageDataForDB);
            string filePath = CreateFilePath(savedImageData, uploadedImage);
            FileStream stream = new FileStream(filePath, FileMode.Create);
            uploadedImage.CopyTo(stream);
            stream.Close();
        }
    
        private string CreatePathForRetrive(string pictureId, string pictureType) 
        {
            byte LenghtOfPictureTypeFirstPart = 6;
            return Path.Combine(_hostEnv.WebRootPath, pictureId) 
                            + "." + pictureType.Substring(LenghtOfPictureTypeFirstPart);
        } 
        
        private string CreateFilePath(Picture savedImageData, IFormFile uploadedImage) 
        {
            byte LenghtOfPictureTypeFirstPart = 6;
            string filePathWithoutType = Path.Combine(_hostEnv.WebRootPath, savedImageData.Id.ToString());
            pictureRepository.SavePicturePath(savedImageData.Id, filePathWithoutType);
            return (filePathWithoutType + "." + uploadedImage.ContentType.ToString().Substring(LenghtOfPictureTypeFirstPart));
        } 
        
    }
}
