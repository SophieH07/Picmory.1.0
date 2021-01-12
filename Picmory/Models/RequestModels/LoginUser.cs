using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Models.RequestModels
{
    public class LoginUser
    {
        public LoginUser() { }
        public LoginUser(string name, string password)
        {
            this.UserName = name;
            this.Password = password;
        }
        public LoginUser(string name, string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }


        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
