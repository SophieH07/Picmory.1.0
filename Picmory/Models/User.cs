using Picmory.Util;
using System.ComponentModel.DataAnnotations;

namespace Picmory.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string EMail { get; set; }
        [Required]
        public string Password { get; set; }
        public Theme Theme { get; set; }
        public string ProfilePicture { get; set; }
        [Timestamp]
        public byte[] RegistrationTime { get; set; }


    }
}
