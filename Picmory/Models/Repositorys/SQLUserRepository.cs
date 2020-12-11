using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                var user = context.Users.Where(a => a.Name == name).Single();
                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool GetUserExist(string name)
        {
            try {
                var user = context.Users.Where(a => a.Name == name).Single();
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
