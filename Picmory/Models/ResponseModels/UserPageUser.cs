using Picmory.Util;
using System.Collections.Generic;

namespace Picmory.Models.RequestResultModels
{
    public class UserPageUser
    {
        public UserPageUser() { }
        public UserPageUser(User user, int followers, int followed,  List<FolderForShow> folders)
        {
            UserName = user.UserName;
            Email = user.Email;
            ColoreOne = user.ColorOne;
            ColoreTwo = user.ColorTwo;
            Followers = followers;
            Followed = followed;
            ProfilePictureId = user.ProfilePictureID;
            Folders = folders;
        }


        public string UserName { get; set; }
        public string Email { get; set; }
        public string ColoreOne { get; set; }
        public string ColoreTwo { get; set; }
        public int Followers { get; set; }
        public int Followed { get; set; }
        public int ProfilePictureId { get; set; }
        public List<FolderForShow> Folders { get; set; }
    }
}
