using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Models.Repositorys
{
    public interface IFollowerRepository
    {
        public void AskNewFollower(int userId, string followerName);
        public void AnswerNewFollower(bool accept, int userId, string followerName);
        public void DeleteFollower(int userId, string followerName);
        public void GetAllFollowers(int userId);
    }
}
