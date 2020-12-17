using System;
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
            return context.Users.Find(id);
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
    }
}
