

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Picmory.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Picture Picture { get; set; }
        [Required]
        [ForeignKey("Tag")]
        public TagName TagName { get; set; }
    }
}
