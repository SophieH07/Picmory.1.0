

using System.ComponentModel.DataAnnotations;

namespace Picmory.Models
{
    public class Followers
    {
        public Followers()
        { }

        public Followers(User Followed, User Follower, bool? Accepted)
        {
            this.Follower = Follower;
            this.Followed = Followed;
            this.Accepted = Accepted;
        }

        [Key]
        public int ID { get; set; }
        public User Followed { get; set; }
        public User Follower { get; set; }
        public bool? Accepted { get; set; }
    }
}
