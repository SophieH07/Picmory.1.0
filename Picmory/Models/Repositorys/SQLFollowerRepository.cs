using Microsoft.EntityFrameworkCore;
using Picmory.Models.RequestResultModels;
using Picmory.Util;
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


        public Success AnswerNewFollower(bool accept, User follower, User followed)
        {
            var follow = context.Followers.FirstOrDefault(item => item.FollowerUser == follower && item.Followed == followed);
            if (follow != null)
            {
                if (follow.Accepted != null) { return Success.FailedByAlreadyAnswered; }
                follow.Accepted = accept;
                context.SaveChanges();
                return Success.Successfull;
            }
            return Success.FailedByNotRequested;
        }

        public Success AskNewFollower(User follower, User followed)
        {
            Follower requested = context.Followers
                    .Include(a => a.FollowerUser)
                    .Where(a => a.FollowerUser == follower && a.Followed == followed)
                    .FirstOrDefault();
            if (requested == null)
            {
                Follower follow = new Follower(followed, follower, null);
                context.Followers.Add(follow);
                context.SaveChanges();
                return Success.Successfull;
            }
            else if (requested.Accepted == null)
                { return Success.FailedByAlreadyRequested; }
            else
                { return Success.FailedByAlreadyFollowed; }
        }

        public Success DeleteFollower(User follower, User followed)
        {
            var follow = context.Followers.FirstOrDefault(item => item.FollowerUser == follower && item.Followed == followed);
            if (follow != null)
            {
                if (follow.Accepted == null) { return Success.FailedByNotAccepted; }
                context.Followers.Remove(follow);
                context.SaveChanges();
                return Success.Successfull;
            }
            return Success.FailedByNotExist;
        }

        public List<string> GetAllFollowers(User user)
        {
            try
            {
                List <string> followerUsers = new List<string>();
                List<Follower> followers = context.Followers
                    .Include(a => a.FollowerUser)
                    .Where(a => a.Followed == user && a.Accepted == true)
                    .ToList();
                foreach (Follower follower in followers)
                {
                    followerUsers.Add(follower.FollowerUser.UserName);
                }
                return followerUsers;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public int GetAllFollowersNumber(User user)
        {
            try
            {
                return context.Followers
                    .Include(a => a.FollowerUser)
                    .Where(a => a.Followed == user && a.Accepted == true)
                    .Count();
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public List<string> GetAllFollowing(User user)
        {
            List<string> followedUsers = new List<string>();
            try
            {   
                List<Follower> followers = context.Followers
                    .Include(a => a.FollowerUser)
                    .Where(a => a.FollowerUser == user && a.Accepted == true)
                    .ToList();
                foreach (Follower follower in followers)
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

        public int GetAllFollowingNumber(User user)
        {
            try
            {
                return context.Followers
                    .Include(a => a.FollowerUser)
                    .Where(a => a.FollowerUser == user && a.Accepted == true)
                    .Count();
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public List<string> GetAllFollowRequest(User user)
        {
            List<string> followRequestUsers = new List<string>();
            try
            {
                List<Follower> followers = context.Followers
                    .Include(a => a.FollowerUser)
                    .Where(a => a.Followed == user && a.Accepted == null)
                    .ToList();
                foreach (Follower follower in followers)
                {
                    followRequestUsers.Add(follower.FollowerUser.UserName);
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
