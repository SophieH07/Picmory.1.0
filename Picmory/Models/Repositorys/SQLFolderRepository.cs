using Picmory.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Models.Repositorys
{
    public class SQLFolderRepository : IFolderRepository
    {
        private PicmoryDbContext context;
        public SQLFolderRepository(PicmoryDbContext context)
        {
            this.context = context;
        }
        
        
        public Folder ChangeFolderAccessType(Folder folder, AccessType newAccessType)
        {
            throw new NotImplementedException();
        }

        public Folder ChangeFolderName(Folder folder, string newName)
        {
            throw new NotImplementedException();
        }

        public Folder DeleteFolder(Folder folder)
        {
            throw new NotImplementedException();
        }

        public List<Folder> GetAllFolders(User user)
        {
            try
            {
                //List<Folder> folders = context.Folders.Where(a => a.Owner == user).ToArray(List<Folder>);
                return null;
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
