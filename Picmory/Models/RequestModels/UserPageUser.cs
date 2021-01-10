using Picmory.Util;
using System.Collections.Generic;

namespace Picmory.Models.RequestResultModels
{
    public class UserPageUser
    {
        public UserPageUser() { }
        public UserPageUser(string name)
        {
            this.UserName = name;
        }
        public UserPageUser(string userName, string email, ThemeColor coloreOne, ThemeColor coloreTwo, List<UserPageUser> followers, int followed, Picture profilePicture, List<FolderForShow> folders)
        {
            UserName = userName;
            Email = email;
            ColoreOne = coloreOne;
            ColoreTwo = coloreTwo;
            Followers = followers;
            Followed = followed;
            ProfilePictureId = profilePicture == null ? 0 : profilePicture.Id;
            Folders = folders;
        }

        public string UserName { get; set; }
        public string Email { get; set; }
        public ThemeColor ColoreOne { get; set; }
        public ThemeColor ColoreTwo { get; set; }
        public List<UserPageUser> Followers { get; set; }
        public int Followed { get; set; }
        public int ProfilePictureId { get; set; }
        public List<FolderForShow> Folders { get; set; }
    }
}
