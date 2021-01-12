using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Models.Repositorys
{
    public interface IUserRepository
    {
        User GetUserData(int id);
        User GetUserData(string name);
        User GetUserDataFromEmail(string email);
        bool EmailAlreadyUsed(string email);
        bool UserNameAlreadyUsed(String name);
        User RegisterNewUser(User user);
        User EditUserData(User user);
    }
}
