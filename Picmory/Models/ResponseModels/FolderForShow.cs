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
        public FolderForShow(string folderName, AccessType access)
        {
            FolderName = folderName;
            Access = access;
        }

      
        public string FolderName { get; set; }
        public AccessType Access { get; set; }
    }
}
