using Picmory.Util;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Picmory.Models
{
    public class Folder
    {
        public Folder() { }
        public Folder(string folderName, AccessType access) 
        {
            FolderName = folderName;
            Access = access;
        }
        public Folder(string folderName, AccessType access, User owner)
        {
            FolderName = folderName;
            Access = access;
            Owner = owner;

        }

        [Key]
        public int Id { get; set; }
        [ForeignKey("PictureOwner")]
        public User Owner { get; set; }
        [Required]
        public string FolderName { get; set; }
        [Required]
        public AccessType Access { get; set; }
    }
}
