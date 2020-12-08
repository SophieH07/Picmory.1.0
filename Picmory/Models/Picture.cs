using Picmory.Util;

namespace Picmory.Models
{
    public class Picture
    {
        public string Path { get; set; }
        public string Description { get; set; }
        public AccessType Access { get; set; }
        public string Type { get; set; }
        public User Owner { get; set; }
        public string FolderName { get; set; }
    }
}
