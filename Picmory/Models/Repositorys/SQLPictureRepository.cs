using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Models.Repositorys
{
    public class SQLPictureRepository : IPictureRepository
    {
        private readonly PicmoryDbContext context;
        public SQLPictureRepository(PicmoryDbContext context)
        {
            this.context = context;
        }
        
        
        public Picture SavePicture(Picture picture)
        {
            context.Pictures.Add(picture);
            context.SaveChanges();
            return picture;
        }

        public bool SavePicturePath(User user, int Id, string path)
        {
            var picture = context.Pictures.FirstOrDefault(item => item.Owner == user && item.Id == Id);
            if (picture != null)
            {
                picture.Path = path;
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
