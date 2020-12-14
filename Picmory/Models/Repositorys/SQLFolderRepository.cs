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
        
       

        public Folder ChangeFolderData(Folder folder)
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
