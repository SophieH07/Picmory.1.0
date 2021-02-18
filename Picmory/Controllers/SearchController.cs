using Microsoft.AspNetCore.Mvc;
using Picmory.Models;
using Picmory.Models.Repositorys;
using Picmory.Models.RequestModels;
using Picmory.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IFolderRepository folderRepository;
        private readonly IPictureRepository pictureRepository;
        private readonly IFollowerRepository followerRepository;
        private readonly UserGet userGet;

        public SearchController(IUserRepository userRepository, IFolderRepository folderRepository, IPictureRepository pictureRepository, IFollowerRepository followerRepository)
        {
            this.userRepository = userRepository;
            this.folderRepository = folderRepository;
            this.pictureRepository = pictureRepository;
            this.followerRepository = followerRepository;
            userGet = new UserGet(userRepository);
        }

        [Produces("application/json")]
        [HttpPost("suggestions")]
        public IActionResult Suggestions([FromBody] string term)
        {
            if (userGet.HaveUser(HttpContext))
            {
                User user = userGet.GetUser(HttpContext);
                if (!term.Contains(" "))
                {

                }
                List <SearchUser> foundUsers = userRepository.GetUsersForTerm(term);
                return Ok(foundUsers);
               
            }
            return Unauthorized();
        }
    }
}
