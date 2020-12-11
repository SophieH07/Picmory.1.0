using Picmory.Util;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Picmory.Models
{
    public class User
    {
        public User() { }
        public User(string name,  string password)
        {
            this.Name = name;
            this.Password = password;
        }
        public User(string name, string email, string password)
        {
            this.Name = name;
            this.EMail = email;
            this.Password = password;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string EMail { get; set; }
        [Required]
        public string Password { get; set; }
        public Theme Theme { get; set; }
        [ForeignKey("ProfPicture")]
        public Picture ProfilePicture { get; set; }
        [Timestamp]
        public byte[] RegistrationTime { get; set; }

    }
}
