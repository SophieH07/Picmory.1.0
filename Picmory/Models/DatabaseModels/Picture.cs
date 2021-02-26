using Picmory.Models.DatabaseModels;
using Picmory.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Picmory.Models 
{
    public class Picture : IDateCreatedAndUpdated
    {
        public Picture() { }
        public Picture(string desription, AccessType accesType, string type, User owner, int folderId) 
        {
            Description = desription;
            Access = accesType;
            Type = type;
            Owner = owner;
            FolderId = FolderId;
        }


        [Key]
        public int Id { get; set; }
        [MaxLength(150)]
        public string Path { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public AccessType Access { get; set; }
        [Required]
        [MaxLength(50)]
        public string Type { get; set; }
        [Required]
        [ForeignKey("PictureOwner")]
        public User Owner { get; set; }
        public int FolderId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public List <Like> Likes { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
