using Picmory.Util;

namespace Picmory.Models.RequestModels
{
    public class ChangeFolderData
    {
        public Folder originalFolder { get; set; }
        public string newName { get; set; }
        public AccessType newAccessType { get; set; }
    }
}
