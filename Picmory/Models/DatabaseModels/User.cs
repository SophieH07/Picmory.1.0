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
    public class User : IDateCreatedAndUpdated
    {
        public User() { }
        public User(string name, string email, string password)
        {
            this.UserName = name;
            this.Email = email;
            this.Password = password;
        }


        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
        [MaxLength(10)]
        public string ColorOne { get; set; }
        [MaxLength(10)]
        public string ColorTwo { get; set; }
        public int ProfilePictureID { get; set; }    
        public List<Folder> Folder { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
