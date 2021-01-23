using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Picmory.Models;
using Picmory.Models.Repositorys;
using Picmory.Models.RequestModels;
using Picmory.Models.RequestResultModels;
using Picmory.Util;

namespace Picmory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IFolderRepository folderRepository;
        private readonly IPictureRepository pictureRepository;
        private readonly IFollowerRepository followerRepository;
        private readonly UserGet userGet;
        private readonly PictureRemover pictureRemover;
       
        public UserController(IUserRepository userRepository, IFolderRepository folderRepository, IPictureRepository pictureRepository, IFollowerRepository followerRepository, IWebHostEnvironment hostEnvironment)
        {
            this.userRepository = userRepository;
            this.folderRepository = folderRepository;
            this.pictureRepository = pictureRepository;
            this.followerRepository = followerRepository;
            userGet = new UserGet(userRepository);
            pictureRemover = new PictureRemover(hostEnvironment, pictureRepository);
        }

        [Produces("application/json")]
        [HttpGet("myinfo")]
        public IActionResult Info()
        {
            if (userGet.HaveUser(HttpContext))
            {
                User user = userGet.GetUser(HttpContext);
                UserPageUser resultUser = new UserPageUser(user, followerRepository.GetAllFollowersNumber(user), followerRepository.GetAllFollowingNumber(user), folderRepository.GetAllFolders(user));
                return Ok(resultUser);
            }
            return Unauthorized();
        }

        [Produces("application/json")]
        [HttpGet("otheruserinfo")]
        public IActionResult OtherUserInfo([FromBody] string userId)
        {
            int.TryParse(userId, out int otheruserId);
            if (userGet.HaveUser(HttpContext))
            {
                User otheruser = userRepository.GetUserData(otheruserId);
                User user = userGet.GetUser(HttpContext);
                UserPageUser resultUser = new UserPageUser(otheruser, followerRepository.GetAllFollowersNumber(otheruser), followerRepository.GetAllFollowingNumber(otheruser), folderRepository.GetAllFoldersForOther(user, otheruser));
                return Ok(resultUser);
            }
            return Unauthorized();
        }


        [HttpPost("changeuserdata")]
        public IActionResult SetNewData([FromBody] ChangeUserData changeData)
        {
            if (userGet.HaveUser(HttpContext))
            {
                User user = userGet.GetUser(HttpContext);

                if (!userRepository.UserNameAlreadyUsed(changeData.UserName) && changeData.UserName != user.UserName && changeData.UserName != null)
                    {  user.UserName = changeData.UserName; }
                else if (userRepository.UserNameAlreadyUsed(changeData.UserName) || changeData.UserName == user.UserName)
                    { return BadRequest("Used Username!"); }
                if (changeData.ColorOne != null)
                    { user.ColorOne = (ThemeColor)changeData.ColorOne; }
                if (changeData.ColorTwo != null)
                    { user.ColorTwo = (ThemeColor)changeData.ColorTwo; }
                if (changeData.Password != null)
                    { user.Password = Hashing.HashPassword(changeData.Password); } 
                if (changeData.ProfilePictureId != 0)
                    {
                        Picture profilePicture = pictureRepository.GetPicture(changeData.ProfilePictureId);
                        if (profilePicture == null) { return BadRequest("Not your picture!"); }
                        if (profilePicture.Owner == user)
                        {
                            user.ProfilePictureID = profilePicture.Id;
                        }
                        return BadRequest("Not your picture!");
                }
                userRepository.EditUserData(user);

                return Ok();                
            }
            return Unauthorized();
        }

        [HttpPost("deleteuser")]
        public IActionResult Deleteuser()
        {
            if (userGet.HaveUser(HttpContext))
            {
                User userToDelete = userGet.GetUser(HttpContext);
                pictureRemover.DeletePictures(userToDelete);
                userRepository.DeleteUser(userToDelete);
                return Ok();
            }
            return Unauthorized();
        }
    }
}
