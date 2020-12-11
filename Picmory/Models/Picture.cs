using Picmory.Util;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Picmory.Models
{
    public class Picture
    {
        public Picture() { }
        public Picture(string path, string desription, AccessType accesType, string type, User owner, string foldername) { }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Path { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public AccessType Access { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        [ForeignKey("PictureOwner")]
        public User Owner { get; set; }
        [Required]
        public string FolderName { get; set; }
    }
}
