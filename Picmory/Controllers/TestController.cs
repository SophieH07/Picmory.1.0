using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Picmory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {

        private IConfiguration _config;
        private IWebHostEnvironment Environment;


        public TestController(IConfiguration config, IWebHostEnvironment _environment)
        {
            _config = config;
            Environment = _environment;
        }

        private static readonly List<string> folder1 = new List<string> { "Profile", "private" };
        private static readonly List<string> folder2 = new List<string> { "Cats", "publicForFollowers" };
        private static readonly List<string> folder3 = new List<string> { "Dogs", "publicForFollowers" };
        private static readonly List<string> folder4 = new List<string> { "Funny", "publicForEveryone" };
        private static readonly Dictionary<string, string> picture1 = new Dictionary<string, string> { { "Id", "1" },{ "Description", "Mercedes AMG Car" }, { "Access", "private" }, { "Folder", "Profile" } };
        private static readonly Dictionary<string, string> picture2 = new Dictionary<string, string> { { "Id", "2" }, { "Description", "Field" }, { "Access", "publicForFollowers" }, { "Folder", "Funny" } };
        private static readonly Dictionary<string, string> picture3 = new Dictionary<string, string> { { "Id", "3" }, { "Description", "Lion" }, { "Access", "publicForEveryone" }, { "Folder", "Cats" } };
        private static readonly Dictionary<string, string> picture4 = new Dictionary<string, string> { { "Id", "4" }, { "Description", "Sea" }, { "Access", "Private" }, { "Folder", "Profile" } };
        private static readonly Dictionary<string, string> picture5 = new Dictionary<string, string> { { "Id", "5" }, { "Description", "Cars" }, { "Access", "Private" }, { "Folder", "Funny" } };
        private static readonly List<List<string>> folders = new List<List<string>> {folder1,folder2,folder3,folder4};
        private static readonly Dictionary<string, object> user = new Dictionary<string, object>
        {
            { "Name" , "Jhon Jhon" },
            { "Theme" , "Light" },
            { "Followers" , "15" },
            { "Followed" , "32" },
            {"Folders",  folders  }
           
        };
        private static readonly List<Dictionary<string, string>> picturesData = new List<Dictionary<string, string>> { picture1, picture2, picture3, picture4, picture5 };


        //Gives the users's data for user page (Name, Theme, Follower, Followed, Folders name's list)
        [HttpGet("user")]
        [Authorize]
        public string Get()
        {
            string data = JsonConvert.SerializeObject(user);
            return data;
        }


        [HttpGet("picturedata/{datastart}")]
        [Authorize]
        public string Get(int datastart)
        {
            string data = JsonConvert.SerializeObject(picturesData);
            return data;
        }


        //Gives a specific picture by it's Id
        [HttpGet("pictures/{pictureId}")]
        [Authorize]
        public IActionResult Get(string pictureId)
        {
            Byte[] picture;
            string path = this.Environment.WebRootPath + "\\"+ pictureId +".jpg";
            picture = System.IO.File.ReadAllBytes(path);
            return File(picture, "image/jpeg");
        }


        //Simple POST request, witch gets username/password in body, and sends back a token
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] string login)
        {
            
            IActionResult response = Unauthorized();
            if (login == "{username : admin, password : admin}")
            {
                var tokenString = GenerateJSONWebToken();
                response = Ok(new { token = tokenString });
            };
            return response;
        }

        //Simple POST request, witch gets username/password/email in body, and sends back a token
        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] string register)
        {

            IActionResult response = Unauthorized();
            if (register == "{username : admin, password : admin, email : admin@gmail.com}")
            {
                var tokenString = GenerateJSONWebToken();
                response = Ok(new { token = tokenString });
            };
            return response;
        }



        private string GenerateJSONWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
