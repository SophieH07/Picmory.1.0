
using System.ComponentModel.DataAnnotations;

namespace Picmory.Models
{
    public class TagName
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string TagData { get; set; }
    }
}
