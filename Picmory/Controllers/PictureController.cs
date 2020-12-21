﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Picmory.Models;
using Picmory.Models.Repositorys;
using Picmory.Models.RequestResultModels;
using Picmory.Util;
using System;
using System.IO;
using System.Linq;

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
        public PictureController(IUserRepository userRepository, IPictureRepository pictureRepository, IWebHostEnvironment hostEnvironment, IFolderRepository folderRepository)
        {
            this.pictureRepository = pictureRepository;
            _hostEnv = hostEnvironment;
            this.folderRepository = folderRepository;
            userGet = new UserGet(userRepository);
        }


        [HttpGet("{pictureId}")]
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

        [HttpPost("uploadPicture")]
        public IActionResult UploadPicture()
        {
            IActionResult response = Unauthorized();

            IFormFile uploadedImage = HttpContext.Request.Form.Files[0];
            Folder folder = folderRepository.GetFolder(userGet.GetUser(HttpContext), HttpContext.Request.Form["FolderName"].ToString());
            UploadPhoto photoData = new UploadPhoto(
                                                HttpContext.Request.Form["Description"].ToString(),
                                                HttpContext.Request.Form["Access"].ToString(),
                                                folder);
            if (userGet.HaveUser(HttpContext) && ModelState.IsValid && uploadedImage != null && ImageTypeIsValid(uploadedImage))
            {
                UploadImage(HttpContext, photoData, uploadedImage);
                response = Ok();
            }
            return response;
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
