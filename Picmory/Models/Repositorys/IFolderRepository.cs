using Picmory.Util;
using System.Collections.Generic;

namespace Picmory.Models.Repositorys
{
    public interface IFolderRepository
    {
        public Folder SaveNewFolder(Folder folder);
        public List<Folder> GetAllFolders(User user);
        public bool ChangeFolderData(User user, Folder originalFolder, string newName, AccessType newAccess);
        public bool DeleteFolder(User user, string folderName);
    }
}
