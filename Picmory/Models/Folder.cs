using Picmory.Util;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Picmory.Models
{
    public class Folder
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("PictureOwner")]
        public User Owner { get; set; }
        [Required]
        public string FolderName { get; set; }
        [Required]
        public AccessType Access { get; set; }
    }
}
