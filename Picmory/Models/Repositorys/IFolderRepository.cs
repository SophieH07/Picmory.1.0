using Picmory.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Models.Repositorys
{
    interface IFolderRepository
    {
        public Folder SaveNewFolder(Folder folder);
        public List<Folder> GetAllFolders(User user);
        public Folder ChangeFolderName(Folder folder, string newName);
        public Folder ChangeFolderAccessType(Folder folder, AccessType newAccessType);
        public Folder DeleteFolder(Folder folder);
    }
}
