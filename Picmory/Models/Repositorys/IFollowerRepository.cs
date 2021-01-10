using Picmory.Models.RequestResultModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Models.Repositorys
{
    public interface IFollowerRepository
    {
        public Followers AskNewFollower(User follower, User followed);
        public bool AnswerNewFollower(bool accept, User follower, User followed);
        public bool DeleteFollower(User follower, User followed);
        public List<UserPageUser> GetAllFollowers(User user);
    }
}
