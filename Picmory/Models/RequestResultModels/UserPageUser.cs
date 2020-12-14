using Picmory.Util;

namespace Picmory.Models.RequestResultModels
{
    public class UserPageUser
    {
        public UserPageUser() { }
        public UserPageUser(string userName, string email, ThemeColor coloreOne, ThemeColor coloreTwo, int followers, int followed, Picture profilePicture ) 
        {
            UserName = userName;
            Email = email;
            ColoreOne = coloreOne;
            ColoreTwo = coloreTwo;
            Followers = followers;
            Followed = followed;
            ProfilePicture = profilePicture;
        }

        public string UserName { get; set; }
        public string Email { get; set; }
        public ThemeColor ColoreOne { get; set; }
        public ThemeColor ColoreTwo { get; set; }
        public int Followers { get; set; }
        public int Followed { get; set; }
        public Picture ProfilePicture { get; set; }
    }
}
