using Picmory.Util;

namespace Picmory.Models.RequestModels
{
    public class ChangeUserData
    {
        public string Password { get; set; }
        public string UserName { get; set; }
        public string? ColorOne { get; set; }
        public string? ColorTwo { get; set; }
        public int ProfilePictureId { get; set; }
        
    }
}
