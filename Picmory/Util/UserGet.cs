using Microsoft.AspNetCore.Http;
using Picmory.Models;
using Picmory.Models.Repositorys;
using System.Linq;

namespace Picmory.Util
{
    public class UserGet
    {
        private IUserRepository userRepository;

        public UserGet(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public User GetUser(HttpContext context)
        {
            return userRepository.GetUserData(int.Parse(context.User.Claims.FirstOrDefault(c => c.Type == "Id").Value));
        }

        public bool HaveUser(HttpContext context)
        {
            return context.User.HasClaim(c => c.Type == "Id");
        }
    }
}
