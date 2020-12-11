using Microsoft.AspNetCore.Mvc;
using Picmory.Models;
using Picmory.Models.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Controllers
{
    public class UserController
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost("registration")]
        public bool Create(string user)
        {
            Console.WriteLine("ok");
            //User newUser = userRepository.RegisterNewUser(user);
            return true;
        }
    }
}
