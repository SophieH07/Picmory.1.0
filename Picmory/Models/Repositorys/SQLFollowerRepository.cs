using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Models.Repositorys
{
    public class SQLFollowerRepository : IFollowerRepository
    {
        public void AnswerNewFollower(bool accept, int userId, string followerName)
        {
            throw new NotImplementedException();
        }

        public void AskNewFollower(int userId, string followerName)
        {
            throw new NotImplementedException();
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
