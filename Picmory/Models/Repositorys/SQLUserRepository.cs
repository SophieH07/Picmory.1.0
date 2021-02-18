using Picmory.Models.RequestModels;
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
        
        
        public User GetUserData(int id)
        {
            try
            { 
                return context.Users.Where(a => a.Id == id).Single();
            }
            catch (InvalidOperationException exc)
            {
                if (exc.Message == "Sequence contains no elements")
                {
                    return null;
                }
                throw;
            }
        }

        public User GetUserData(string name)
        {
            try
            {
                return context.Users.Where(a => a.UserName == name).Single();
            }
            catch (InvalidOperationException exc)
            {
                if (exc.Message == "Sequence contains no elements")
                {
                    return null;
                }
                throw;
            }
        }

        public User GetUserDataFromEmail(string email)
        {
            try
            {
                var user = context.Users.Where(a => a.Email == email).Single();
                return user;
            }
            catch (InvalidOperationException exc)
            {
                if (exc.Message == "Sequence contains no elements")
                {
                    return null;
                }
                throw;
            }
        }

        public bool EmailAlreadyUsed(string email)
        {
            try
            {
                var user = context.Users.Where(a => a.Email == email).Single();
                return true;
            }
            catch (InvalidOperationException exc)
            {
                if (exc.Message == "Sequence contains no elements")
                {
                    return false;
                }
                throw;
            }
        }

        public bool UserNameAlreadyUsed(string name)
        {
            try {
                var user = context.Users.Where(a => a.UserName == name).Single();
                return true;
            }
            catch (InvalidOperationException exc)
            {
                if (exc.Message == "Sequence contains no elements")
                {
                    return false;
                }
                throw;
            }
        }
        

        public User RegisterNewUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }
       
        public User EditUserData(User userChanges)
        {
            var user = context.Users.Attach(userChanges);
            user.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return userChanges;
        }

        public void DeleteUser(User userToDelete)
        {
            context.Users.Remove(userToDelete);
            context.SaveChanges(); 
        }


        public List<SearchUser> GetUsersForTerm(string term)
        {
            List<SearchUser> resultUsers = new List<SearchUser>();
            try
            {
                return resultUsers = context.Users
                    .Where(a => a.UserName
                    .Contains(term))
                    .Select(a => new SearchUser { UserName = a.UserName, PictureId = a.ProfilePictureID }).Take(5).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
