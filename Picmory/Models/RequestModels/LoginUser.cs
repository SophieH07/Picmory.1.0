using System.ComponentModel.DataAnnotations;

namespace Picmory.Models.RequestModels
{
    public class LoginUser
    {
        public LoginUser() { }
        public LoginUser(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
