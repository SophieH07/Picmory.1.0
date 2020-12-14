﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Models.Repositorys
{
    public interface IUserRepository
    {
        User GetUserData(int id);
        User GetUserData(string name);
        bool GetUserExist(String name);
        User RegisterNewUser(User user);
        User EditUserData(User user);
    }
}