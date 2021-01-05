using Picmory.Util;

namespace Picmory.Models.RequestModels
{
    public class ChangeUserData
    {
        public string UserName { get; set; }
        public AccessType ColorOne { get; set; }
        public AccessType ColorTwo { get; set; }
        
    }
}
