﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Models.RequestModels
{
    public class SearchUser 
    {
        public SearchUser()
        {

        }
        public SearchUser(String UserName, int? PictureId)
        {
            this.UserName = UserName;
            this.PictureId = PictureId;
        }

        public String UserName { get; set; }
        public int? PictureId { get; set; }

       
        public void Add(string username, int id)
        {
            this.UserName = username;
            this.PictureId = id;
        }
    }
}
