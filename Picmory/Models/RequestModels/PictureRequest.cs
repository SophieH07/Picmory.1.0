﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Models.RequestModels
{
    public class PictureRequest
    {
        public int UserId { get; set; }
        public string FolderName { get; set; }
        public int Offset { get; set; }
    }
}
