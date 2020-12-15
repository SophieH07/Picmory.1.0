using Microsoft.AspNetCore.Http;
using Picmory.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Models.RequestResultModels
{
    public class UploadPhoto
    {
        public UploadPhoto() { }
        public UploadPhoto(string description, string access, string folderName)
        {
            Description = description;
            Access = (AccessType)Enum.Parse(typeof(AccessType), access, true);
            FolderName = folderName;
        }



        [Required]
        public string Description { get; set; }
        [Required]
        public AccessType Access { get; set; }
        [Required]
        public string FolderName { get; set; }
    }
}
