using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Models.RequestModels
{
    public class FollowerAccept
    {
        public string UserName { get; set; }
        public bool Accept { get; set; }
    }
}
