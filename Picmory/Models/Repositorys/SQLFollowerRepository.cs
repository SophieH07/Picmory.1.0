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

        public List<string> GetAllFollowers(User user)
        {
            try
            {
                List <string> followerUsers = new List<string>();
                List<Followers> followers = context.Followers
                    .Include(a => a.Follower)
                    .Where(a => a.Followed == user && a.Accepted == true)
                    .ToList();
                foreach (Followers follower in followers)
                {
                    followerUsers.Add(follower.Follower.UserName);
                }
                return followerUsers;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<string> GetAllFollowing(User user)
        {
            List<string> followedUsers = new List<string>();
            try
            {   
                List<Followers> followers = context.Followers
                    .Include(a => a.Follower)
                    .Where(a => a.Follower == user && a.Accepted == true)
                    .ToList();
                foreach (Followers follower in followers)
                {
                    followedUsers.Add(follower.Followed.UserName);
                }
                return followedUsers;
            }
            catch (Exception e)
            {
                return followedUsers;
            }
        }

        public List<string> GetAllFollowRequest(User user)
        {
            List<string> followRequestUsers = new List<string>();
            try
            {
                List<Followers> followers = context.Followers
                    .Include(a => a.Follower)
                    .Where(a => a.Followed == user && a.Accepted == null)
                    .ToList();
                foreach (Followers follower in followers)
                {
                    followRequestUsers.Add(follower.Follower.UserName);
                }
                return followRequestUsers;
            }
            catch (Exception e)
            {
                return followRequestUsers;
            }
        }
    }
}
