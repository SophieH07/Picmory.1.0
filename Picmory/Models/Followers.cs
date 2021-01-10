

using System.ComponentModel.DataAnnotations;

namespace Picmory.Models
{
    public class Followers
    {
        [Key]
        public int ID { get; set; }
        public User Followed { get; set; }
        public User Follower { get; set; }
        public bool Accepted { get; set; }
    }
}
