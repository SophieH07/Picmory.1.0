using Picmory.Util;
using System;

namespace Picmory.Models.RequestModels
{
    public class NavBarUser
    {
        public NavBarUser()
        {}
        public NavBarUser(string userName, Picture profilePicture, ThemeColor colorOne, ThemeColor colorTwo)
        {
            this.UserName = userName;
            this.PictureId = profilePicture==null ? 0 :  profilePicture.Id;
            this.ColorOne = colorOne;
            this.ColorTwo = colorTwo;
        }
       
        public String UserName { get; set; }
        public int? PictureId { get; set; }
        public ThemeColor ColorOne { get; set; }
        public ThemeColor ColorTwo { get; set; }
    }
}
