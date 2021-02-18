

using System.ComponentModel.DataAnnotations;

namespace Picmory.Models
{
    public class Follower
    {
        public Follower()
        { }

        public Follower(User Followed, User Follower, bool? Accepted)
        {
            this.FollowerUser = Follower;
            this.Followed = Followed;
            this.Accepted = Accepted;
        }

        [Key]
        public int ID { get; set; }
        [Required]
        public User Followed { get; set; }
        [Required]
        public User FollowerUser { get; set; }
        public bool? Accepted { get; set; }
    }
}
