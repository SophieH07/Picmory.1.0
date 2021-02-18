using Picmory.Util;
using System;
using System.Collections.Generic;
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
        [Required]
        [ForeignKey("FolderOwner")]
        public User Owner { get; set; }
        [Required]
        [MaxLength(50)]
        public string FolderName { get; set; }
        [Required]
        public AccessType Access { get; set; }
        public List<Picture> Pictures { get; set; }
    }
}
