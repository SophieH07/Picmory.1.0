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
        [HttpGet("myuserinfo")]
        public IActionResult OtherUserInfo()
        {
            if (userGet.HaveUser(HttpContext))
            {
                User user = userGet.GetUser(HttpContext);
                return Ok(new UserPageUser(user, followerRepository.GetAllFollowersNumber(user), followerRepository.GetAllFollowingNumber(user), folderRepository.GetAllFolders(user)));
                //User otheruser = userRepository.GetUserData(userId);
                //return Ok(new UserPageUser(otheruser, followerRepository.GetAllFollowersNumber(otheruser), followerRepository.GetAllFollowingNumber(otheruser), folderRepository.GetAllFoldersForOther(user, otheruser)));
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
                        if (profilePicture.Owner != user) { return BadRequest("Not your picture!"); }
                        else 
                            {
                                user.ProfilePictureID = profilePicture.Id;
                            }
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
