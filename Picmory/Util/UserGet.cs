using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Picmory.Models;
using Picmory.Models.Repositorys;
using System.IdentityModel.Tokens.Jwt;
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
            SecurityToken jsonToken;
            var handler = new JwtSecurityTokenHandler();
            string cookie = context.Request.Cookies["Bearer"];
            jsonToken = handler.ReadToken(cookie);
            JwtSecurityToken tokenS = handler.ReadToken(cookie) as JwtSecurityToken;
            string id = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            return userRepository.GetUserData(int.Parse(id));
        }

        public bool HaveUser(HttpContext context)
        {
            SecurityToken jsonToken;
            var handler = new JwtSecurityTokenHandler();
            string cookie = context.Request.Cookies["Bearer"];
            if (cookie.Length != 244) { return false; }
            try { jsonToken = handler.ReadToken(cookie); }
            catch { return false; }
            JwtSecurityToken tokenS = handler.ReadToken(cookie) as JwtSecurityToken;
            string id = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            if (id == null) { return false; }
            return true;
        }
    }
}
