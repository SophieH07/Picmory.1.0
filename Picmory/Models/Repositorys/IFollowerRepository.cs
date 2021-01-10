﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Models.Repositorys
{
    public interface IFollowerRepository
    {
        public Followers AskNewFollower(User follower, User followed);
        public bool AnswerNewFollower(bool accept, User follower, User followed);
        public void DeleteFollower(int userId, string followerName);
        public void GetAllFollowers(int userId);
    }
}
