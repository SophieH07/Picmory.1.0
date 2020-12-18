using Picmory.Util;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Picmory.Models
{
    public class Picture
    {
        public Picture() { }
        public Picture(string desription, AccessType accesType, string type, User owner, Folder folder) 
        {
            Description = desription;
            Access = accesType;
            Type = type;
            Owner = owner;
            Folder = folder;
        }


        [Key]
        public int Id { get; set; }
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
        [ForeignKey("FolderData")]
        public Folder Folder { get; set; }
        [Timestamp]
        [Column("UploadDate")]
        public byte[] UploadDate { get; set; }
    }
}
