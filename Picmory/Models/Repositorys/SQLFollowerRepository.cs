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


        public void AnswerNewFollower(bool accept, int userId, string followerName)
        {
            throw new NotImplementedException();
        }

        public Followers AskNewFollower(User follower, User followed)
        {
            Followers follow = new Followers(followed, follower, false);
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
