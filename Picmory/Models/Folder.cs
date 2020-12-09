using Picmory.Util;

namespace Picmory.Models
{
    public class Folder
    {
        public User Owner { get; set; }
        public string FolderName { get; set; }
        public AccessType Access { get; set; }
    }
}
