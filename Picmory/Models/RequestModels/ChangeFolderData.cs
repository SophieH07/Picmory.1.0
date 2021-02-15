using Picmory.Util;

namespace Picmory.Models.RequestModels
{
    public class ChangeFolderData
    {
        public ChangeFolderData originalFolder { get; set; }
        public string Name { get; set; }
        public AccessType? Access { get; set; }
        public User Owner { get; set; }
    }
}
