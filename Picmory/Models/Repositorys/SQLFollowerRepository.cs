using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Models.Repositorys
{
    public class SQLFollowerRepository : IFollowerRepository
    {
        private PicmoryDbContext context;
        public SQLFollowerRepository(PicmoryDbContext context)
        {
            this.context = context;
        }


        public bool AnswerNewFollower(bool accept, User follower, User followed)
        {
            var follow = context.Followers.FirstOrDefault(item => item.Follower == follower && item.Followed == followed);
            if (follow != null)
            {
                follow.Accepted = accept;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public Followers AskNewFollower(User follower, User followed)
        {
            Followers follow = new Followers(followed, follower, null);
            context.Followers.Add(follow);
            context.SaveChanges();
            return follow;
        }

        public void DeleteFollower(int userId, string followerName)
        {
            throw new NotImplementedException();
        }

        public void GetAllFollowers(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
