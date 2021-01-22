using Picmory.Models.RequestModels;
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
        
       

        public Success ChangeFolderData(User user, ChangeFolderData originalFolder, string newName, AccessType? newAccess)
        {
            Folder folder = context.Folders
                .FirstOrDefault(item => item.Owner == user &&
                                        item.FolderName == originalFolder.Name &&
                                        item.Access == originalFolder.Access);
            if (folder != null)
            {
                if (context.Folders.Where(a => a.Owner == user && a.FolderName == newName).SingleOrDefault() == null) 
                {
                    if (newName != null) { folder.FolderName = newName; }
                    if (newAccess != null) { folder.Access = (AccessType)newAccess; };
                    context.SaveChanges();
                    return Success.Successfull;};
                return Success.FailedByUsedName;
            }
            return Success.FailedByNotExist;
        }

        public Success DeleteFolder(User user, string folderName)
        {
            var folder = context.Folders.FirstOrDefault(item => item.Owner == user && item.FolderName == folderName);
            if (folder != null)
            {
                context.Folders.Remove(folder);
                context.SaveChanges();
                return Success.Successfull;
            }
            return Success.FailedByNotExist;
        }

        public List<FolderForShow> GetAllFolders(User user)
        {

            try
            {
                List<Folder> folders =  context.Folders.Where(a => a.Owner == user).ToList();
                List<FolderForShow> foldersForShow = new List<FolderForShow>();
                foreach (Folder folder in folders)
                {
                    foldersForShow.Add(new FolderForShow(folder.FolderName, folder.Access));
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

        public Success SaveNewFolder(Folder folder)
        {
            if (context.Folders.Where(a => a.Owner == folder.Owner && a.FolderName == folder.FolderName).SingleOrDefault() == null)
            {
                context.Folders.Add(folder);
                context.SaveChanges();
                return Success.Successfull;
            }
            return Success.FailedByUsedName;
        }
    }
}
