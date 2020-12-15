using Picmory.Util;
using System.Collections.Generic;

namespace Picmory.Models.Repositorys
{
    public interface IFolderRepository
    {
        public Folder SaveNewFolder(Folder folder);
        public List<Folder> GetAllFolders(User user);
        public bool ChangeFolderName(User user, string originalName, string newName);
        public bool ChangeFolderAccess(User user, string folderName, AccessType newAccess);
        public bool DeleteFolder(User user, string folderName);
    }
}
