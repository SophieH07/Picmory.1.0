using Picmory.Util;
using System;

namespace Picmory.Models.RequestModels
{
    public class NavBarUser
    {
        public NavBarUser()
        {}
        public NavBarUser(string userName, Picture profilePicture, ThemeColor coloreOne, ThemeColor coloreTwo)
        {
            this.UserName = userName;
            this.PictureId = profilePicture==null ? 0 :  profilePicture.Id;
            this.ColoreOne = coloreOne;
            this.ColoreTwo = coloreTwo;
        }
       
        public String UserName { get; set; }
        public int? PictureId { get; set; }
        public ThemeColor ColoreOne { get; set; }
        public ThemeColor ColoreTwo { get; set; }
    }
}
