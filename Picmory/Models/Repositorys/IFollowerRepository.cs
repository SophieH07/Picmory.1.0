using Picmory.Models.RequestResultModels;
using Picmory.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Models.Repositorys
{
    public interface IFollowerRepository
    {
        public Success AskNewFollower(User follower, User followed);
        public Success AnswerNewFollower(bool accept, User follower, User followed);
        public Success DeleteFollower(User follower, User followed);
        public List<string> GetAllFollowers(User user);
        public List<string> GetAllFollowing(User user);
        public List<string> GetAllFollowRequest(User user);
    }
}
