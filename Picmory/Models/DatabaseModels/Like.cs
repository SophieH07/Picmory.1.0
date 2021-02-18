using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Picmory.Models
{
    public class Like
    {
        public Like()
        {
        }

        public Like(User Owner, Picture Picture)
        {
            this.Owner = Owner;
            this.Picture = Picture;
        }


        [Key]
        public int Id { get; set; }
        [ForeignKey("LikeOwner")]
        public User Owner { get; set; }
        [Required]
        public Picture Picture { get; set; }
    }
}
