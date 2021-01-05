﻿using Picmory.Models.RequestResultModels;
using Picmory.Util;
using System.Collections.Generic;

namespace Picmory.Models.Repositorys
{
    public interface IFolderRepository
    {
        public Folder SaveNewFolder(Folder folder);
        public List<FolderForShow> GetAllFolders(User user);
        public Folder GetFolder(User user, string folderName);
        public bool ChangeFolderData(User user, Folder originalFolder, string newName, AccessType newAccess);
        public bool DeleteFolder(User user, string folderName);
    }
}
