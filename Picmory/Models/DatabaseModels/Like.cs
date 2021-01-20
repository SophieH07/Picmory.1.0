using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Picmory.Models
{
    public class Like
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("LikeOwner")]
        public User Owner { get; set; }
        public int PictureId { get; set; }
    }
}
