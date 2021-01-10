using Microsoft.EntityFrameworkCore;
using Picmory.Models.RequestResultModels;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public bool DeleteFollower(User follower, User followed)
        {
            var follow = context.Followers.FirstOrDefault(item => item.Follower == follower && item.Followed == followed);
            if (follow != null)
            {
                context.Followers.Remove(follow);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<UserPageUser> GetAllFollowers(User user)
        {
            try
            {
                List <UserPageUser> followerUsers = new List<UserPageUser>();
                List<Followers> followers = context.Followers.Include(a => a.Follower).Where(a => a.Followed == user).ToList();
                foreach (Followers follower in followers)
                {
                    followerUsers.Add(new UserPageUser(follower.Follower.UserName));
                }
                return followerUsers;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
