using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Picmory.Models.Repositorys
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly PicmoryDbContext context;
        public SQLUserRepository(PicmoryDbContext context)
        {
            this.context = context;
        }
        
        
        public User EditUserData(User userChanges)
        {
            var user = context.Users.Attach(userChanges);
            user.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return userChanges;
        }

        public User GetUserData(int id)
        {
            return context.Users.Include(a => a.ProfilePicture).Where(a => a.Id == id).Single();
        }

        public User GetUserData(string name)
        {
            try
            {
                var user = context.Users.Where(a => a.UserName == name).Single();
                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public User GetUserDataFromEmail(string email)
        {
            try
            {
                var user = context.Users.Where(a => a.Email == email).Single();
                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool EmailAlreadyUsed(string email)
        {
            try
            {
                var user = context.Users.Where(a => a.Email == email).Single();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UserNameAlreadyUsed(string name)
        {
            try {
                var user = context.Users.Where(a => a.UserName == name).Single();
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }

        public User RegisterNewUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }

        public List<User> GetUsersForTerm(string term)
        {
            List<User> resultUsers = new List<User>();
            try
            {
                return resultUsers = context.Users.Where(a => a.UserName.Contains(term)).Include(a => a.ProfilePicture).Take(5).ToList();
               
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
