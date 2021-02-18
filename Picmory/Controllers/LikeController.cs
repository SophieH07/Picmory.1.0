using Microsoft.AspNetCore.Mvc;
using Picmory.Models;
using Picmory.Models.Repositorys;
using Picmory.Models.Repositorys.Interfaces;
using Picmory.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LikeController :ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IPictureRepository pictureRepository;
        private readonly ILikeRepository likeRepository;
        private readonly UserGet userGet;

        public LikeController(IUserRepository userRepository, IPictureRepository pictureRepository, ILikeRepository likeRepository)
        {
            this.userRepository = userRepository;
            this.pictureRepository = pictureRepository;
            this.likeRepository = likeRepository;
            userGet = new UserGet(userRepository);
        }


        [HttpPost("like")]
        public IActionResult Like([FromBody] int pictureId)
        {
            if (userGet.HaveUser(HttpContext))
            {
                User user = userGet.GetUser(HttpContext);
                return Ok(likeRepository.SaveLike(pictureId, user));
            }
            return Unauthorized();
        }

        [HttpPost("deletelike")]
        public IActionResult DeleteLike([FromBody] int pictureId)
        {
            if (userGet.HaveUser(HttpContext))
            {
                User user = userGet.GetUser(HttpContext);
                return Ok(likeRepository.SaveDislike(pictureId, user));
            }
            return Unauthorized();
        }
    }
}
