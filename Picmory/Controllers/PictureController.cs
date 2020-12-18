﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Picmory.Models;
using Picmory.Models.Repositorys;
using Picmory.Models.RequestResultModels;
using System;
using System.Collections.Generic;
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
        private readonly IUserRepository userRepository;
        private readonly IPictureRepository pictureRepository;
        private readonly IWebHostEnvironment _hostEnv;
        public PictureController(IUserRepository userRepository, IPictureRepository pictureRepository, IWebHostEnvironment hostEnvironment)
        {
            this.userRepository = userRepository;
            this.pictureRepository = pictureRepository;
            _hostEnv = hostEnvironment;
        }


        [HttpGet("{pictureId}")]
        public IActionResult GetImageById(string pictureId)
        {
            string pictureType = pictureRepository.GetPictureType(int.Parse(pictureId));
            if (HaveUser(HttpContext) && pictureType != null)
            {
                string path = CreatePathForRetrive(HttpContext, pictureId, pictureType);
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
            UploadPhoto photoData = new UploadPhoto(
                                                HttpContext.Request.Form["Description"].ToString(),
                                                HttpContext.Request.Form["Access"].ToString(),
                                                HttpContext.Request.Form["FolderName"].ToString());
            if (HaveUser(HttpContext) && ModelState.IsValid && uploadedImage != null && ImageTypeIsValid(uploadedImage))
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

        private User GetUser(HttpContext context)
        {
            return userRepository.GetUserData(int.Parse(context.User.Claims.FirstOrDefault(c => c.Type == "Id").Value));
        }

        private bool HaveUser(HttpContext context)
        {
            return context.User.HasClaim(c => c.Type == "Id");
        }
    
        private void UploadImage(HttpContext context, UploadPhoto photoData, IFormFile uploadedImage)
        {
            string uploadFolder = CreateFolderPath(context);
            Picture imageDataForDB = new Picture(
                                            photoData.Description,
                                            photoData.Access,
                                            uploadedImage.ContentType,
                                            GetUser(context),
                                            photoData.FolderName);
            Picture savedImageData = pictureRepository.SavePicture(imageDataForDB);
            string filePath = CreateFilePath(uploadFolder, savedImageData, uploadedImage);
            FileStream stream = new FileStream(filePath, FileMode.Create);
            uploadedImage.CopyTo(stream);
            stream.Close();
        }
    
        private string CreatePathForRetrive(HttpContext context, string pictureId, string pictureType) 
        {
            byte LenghtOfPictureTypeFirstPart = 6;
            User user = GetUser(context);
            return Path.Combine(_hostEnv.WebRootPath, user.UserName, pictureId) 
                            + "." + pictureType.Substring(LenghtOfPictureTypeFirstPart);
        } 
        
        private string CreateFilePath(string uploadFolder, Picture savedImageData, IFormFile uploadedImage) 
        {
            byte LenghtOfPictureTypeFirstPart = 6;
            string filePathWithoutType = Path.Combine(uploadFolder, savedImageData.Id.ToString());
            pictureRepository.SavePicturePath(savedImageData.Id, filePathWithoutType);
            return (filePathWithoutType + "." + uploadedImage.ContentType.ToString().Substring(LenghtOfPictureTypeFirstPart));
        } 
        
        private string CreateFolderPath(HttpContext context) 
        {
            User user = GetUser(context);
            return Path.Combine(_hostEnv.WebRootPath, user.UserName);
        }
    }
}
