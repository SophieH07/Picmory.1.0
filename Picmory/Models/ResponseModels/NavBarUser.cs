using Picmory.Util;
using System;

namespace Picmory.Models.RequestModels
{
    public class NavBarUser
    {
        public NavBarUser()
        {}
        public NavBarUser(User user)
        {
            this.UserName = user.UserName;
            this.PictureId = user.ProfilePictureID;
            this.ColorOne = user.ColorOne;
            this.ColorTwo = user.ColorTwo;
        }
       

        public string UserName { get; set; }
        public int? PictureId { get; set; }
        public string ColorOne { get; set; }
        public string ColorTwo { get; set; }
    }
}
