using Picmory.Util;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Picmory.Models
{
    public class User
    {
        public User() { }
        public User(ThemeColor colorOne, ThemeColor coloreTwo) 
        {
            ColorOne = colorOne;
            ColorTwo = coloreTwo;
        }
        public User(string name,  string password)
        {
            this.UserName = name;
            this.Password = password;
        }
        public User(string name, string email, string password)
        {
            this.UserName = name;
            this.Email = email;
            this.Password = password;
        }


        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public ThemeColor ColorOne { get; set; }
        public ThemeColor ColorTwo { get; set; }
        [ForeignKey("ProfPicture")]
        public Picture ProfilePicture { get; set; }
        [Timestamp]
        [Column("RegistrationDate")]
        public byte[] RegistrationTime { get; set; }

    }
}
