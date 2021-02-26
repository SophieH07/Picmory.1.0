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
using Picmory.Models.Repositorys.Interfaces;

namespace Picmory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PictureController :ControllerBase
    {
        private readonly string[] AcceptedFileTypes = { "image/gif", "image/png", "image/jpg", "image/jpeg" };
        private readonly IPictureRepository pictureRepository;
        private readonly IFolderRepository folderRepository;
        private readonly IUserRepository userRepository;
        private readonly ILikeRepository likeRepository;
        private readonly UserGet userGet;
        private readonly IWebHostEnvironment _hostEnv;


        public PictureController(IUserRepository userRepository, IPictureRepository pictureRepository, IWebHostEnvironment hostEnvironment, IFolderRepository folderRepository, ILikeRepository likeRepository)
        {
            this.userRepository = userRepository;
            this.pictureRepository = pictureRepository;
            this.likeRepository = likeRepository;
            _hostEnv = hostEnvironment;
            this.folderRepository = folderRepository;
            userGet = new UserGet(userRepository);
        }


        [HttpGet("picture/{pictureid}")]
        public IActionResult GetImageById(string pictureId)
        {
            if (pictureId == "0") { 
                return File(System.IO.File.ReadAllBytes(_hostEnv.WebRootPath + "/0.jpg"), "image/jpg"); }
            Picture pictureInDB = pictureRepository.GetPicture(int.Parse(pictureId));
            if (userGet.HaveUser(HttpContext) && pictureInDB.Type != null)
            {
                User user = userGet.GetUser(HttpContext);
                if (pictureInDB.Access != AccessType.Private || (user.Id == pictureInDB.Owner.Id) ) { 
                string path = CreatePathForRetrive(pictureId, pictureInDB.Type);
                Byte[] picture = System.IO.File.ReadAllBytes(path);
                return File(picture, pictureInDB.Type);
                }
                return BadRequest("Private picture!");
            }
            return Unauthorized();
        }

        [HttpPost("uploadpicture")]
        public IActionResult UploadPicture()
        {
            IFormFile uploadedImage = HttpContext.Request.Form.Files[0];
            Folder folder = folderRepository.GetFolder(userGet.GetUser(HttpContext), HttpContext.Request.Form["FolderName"].ToString());
            if (folder == null) { return BadRequest("Not existing folder!"); }
            UploadPhoto photoData = new UploadPhoto(
                                                HttpContext.Request.Form["Description"].ToString(),
                                                HttpContext.Request.Form["Access"].ToString(),
                                                folder);
            if (userGet.HaveUser(HttpContext) && ModelState.IsValid && uploadedImage != null && ImageTypeIsValid(uploadedImage) && ImageDataIsValid(photoData))
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
                    Success result = ValidateAccess(changeData, picture);
                    switch (result)
                    {
                        case Success.Successfull:
                            pictureRepository.ChangePictureData(changeData);
                            return Ok();
                        case Success.FailedByWrongAccessFolder:
                            return BadRequest("Wrong access picture for " + " folder!");
                        case Success.FailedByWrongAccessNewFolder:
                            return BadRequest("Wrong access picture for " + changeData.FolderName + " folder!");
                        case Success.FailedByNotExistFolderName:
                            return BadRequest("Don't have " + changeData.FolderName + " folder");
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
                        System.IO.File.Delete(_hostEnv.WebRootPath + "/" + pictureId.ToString() + "." + picture.Type.Substring(6));
                        return Ok();
                    }
                }
                return BadRequest("Not your picture!");
            }
            return Unauthorized();
        }



        [HttpPost("getmyimages")]
        [Produces("application/json")]
        public IActionResult GetImageForMe(PictureRequest data)
        {
            if (userGet.HaveUser(HttpContext))
            {
                List<ResponsePicture> pictures =  pictureRepository.GetPicturesForMe(userGet.GetUser(HttpContext), data.Offset, data.FolderName);
                foreach (ResponsePicture p in pictures)
                {
                    p.Likes = likeRepository.ListOfLikers(p.Id);
                }
                return Ok(pictures);
            }
            return Unauthorized();
        }

        [HttpPost("getotherimages")]
        [Produces("application/json")]
        public IActionResult GetImageForUser(PictureRequest data)
        {
            if (userGet.HaveUser(HttpContext))
            {
                User otherUser = userRepository.GetUserData(data.UserId);
                if (otherUser == null) { return BadRequest("Not existing user!"); }
                List<ResponsePicture> pictures = pictureRepository.GetPicturesFromOther(userGet.GetUser(HttpContext), otherUser, data.Offset, data.FolderName);
                return Ok(JsonConvert.SerializeObject(pictures));
            }
            return Unauthorized();
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
                                            photoData.Folder.Id);
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

        private Success ValidateAccess(PictureChange changeData, Picture picture)
        {
            if (changeData.FolderName != null && changeData.Access != null)
            {
                Folder newFolder = folderRepository.GetFolder(picture.Owner, changeData.FolderName);
                if (newFolder == null) { return Success.FailedByNotExistFolderName; }
                else if (!(changeData.Access <= newFolder.Access)) { return Success.FailedByWrongAccessNewFolder; }
                else { return Success.Successfull; }
            }
            else if (changeData.FolderName != null && changeData.Access == null) 
            {
                Folder newFolder = folderRepository.GetFolder(picture.Owner, changeData.FolderName);
                if (newFolder == null) { return Success.FailedByNotExistFolderName; }
                else if (!(picture.Access <= newFolder.Access)) { return Success.FailedByWrongAccessNewFolder; }
                else { return Success.Successfull; }
            }
            else if (changeData.FolderName == null && changeData.Access != null) 
            {
                if (!(changeData.Access <= folderRepository.GetFolder(picture.FolderId).Access)) { return Success.FailedByWrongAccessFolder; }
                else { return Success.Successfull;  }
            }
            else
            {
                return Success.Successfull;
            }
        }
        
        private bool ImageDataIsValid(UploadPhoto photoData)
        {
            if (photoData.Folder.Access >= photoData.Access) { return true; }
            else { return false; }
        }
    }
}
