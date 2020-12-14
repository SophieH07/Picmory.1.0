using Picmory.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Models.RequestResultModels
{
    public class FolderForShow
    {
        public FolderForShow() { }
        public FolderForShow(string folderName, AccessType access, string owner)
        {
            FolderName = folderName;
            Access = access;
            Owner = owner;

        }

      
        public string Owner { get; set; }
        public string FolderName { get; set; }
        public AccessType Access { get; set; }
    }
}
