using Picmory.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Models.Repositorys
{
    public interface IFolderRepository
    {
        public Folder SaveNewFolder(Folder folder);
        public List<Folder> GetAllFolders(User user);
        public Folder ChangeFolderData(Folder folder);
        public Folder DeleteFolder(Folder folder);
    }
}
