using Picmory.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Picmory.Models.Repositorys
{
    public class SQLFolderRepository : IFolderRepository
    {
        private PicmoryDbContext context;
        public SQLFolderRepository(PicmoryDbContext context)
        {
            this.context = context;
        }
        
       

        public bool ChangeFolderName(User user, string originalName, string newName)
        {
            var folder = context.Folders.FirstOrDefault(item => item.Owner == user && item.FolderName==originalName);
            if (folder != null)
            {
                folder.FolderName = newName;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool ChangeFolderAccess(User user, string folderName, AccessType newAccess)
        {
            var folder = context.Folders.FirstOrDefault(item => item.Owner == user && item.FolderName==folderName);
            if (folder != null)
            {
                folder.Access = newAccess;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteFolder(User user, string folderName)
        {
            var folder = context.Folders.FirstOrDefault(item => item.Owner == user && item.FolderName == folderName);
            if (folder != null)
            {
                context.Folders.Remove(folder);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Folder> GetAllFolders(User user)
        {
            try
            {
                return  context.Folders.Where(a => a.Owner == user).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Folder SaveNewFolder(Folder folder)
        {
            context.Folders.Add(folder);
            context.SaveChanges();
            return folder;
        }
    }
}
