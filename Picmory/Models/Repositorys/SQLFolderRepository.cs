using Picmory.Models.RequestResultModels;
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
        
       

        public bool ChangeFolderData(User user, Folder originalFolder, string newName, AccessType? newAccess)
        {
            var folder = context.Folders.FirstOrDefault(item => item.Owner == user && item.FolderName==originalFolder.FolderName);
            if (folder != null)
            {
                if (newName != null) { folder.FolderName = newName; }
                if (newAccess != null) { folder.Access = (AccessType)newAccess; } ;
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

        public List<FolderForShow> GetAllFolders(User user)
        {

            try
            {
                List<Folder> folders =  context.Folders.Where(a => a.Owner == user).ToList();
                List<FolderForShow> foldersForShow = new List<FolderForShow>();
                foreach (Folder folder in folders)
                {
                    foldersForShow.Add(new FolderForShow(folder.FolderName, folder.Access, user.UserName));
                }
                return foldersForShow;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Folder GetFolder(User user, string folderName)
        {
            try
            {
                return context.Folders.Where(a => a.Owner == user && a.FolderName == folderName).SingleOrDefault();
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
