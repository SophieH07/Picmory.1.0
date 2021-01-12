using Picmory.Util;

namespace Picmory.Models.RequestModels
{
    public class ChangeUserData
    {
        public string UserName { get; set; }
        public ThemeColor? ColorOne { get; set; }
        public ThemeColor? ColorTwo { get; set; }
        
    }
}
