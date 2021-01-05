using System;
using System.Collections.Generic;
using System.Linq;

namespace Picmory.Models.Repositorys
{
    public class SQLPictureRepository : IPictureRepository
    {
        private readonly PicmoryDbContext context;
        public SQLPictureRepository(PicmoryDbContext context)
        {
            this.context = context;
        }

        public Picture GetPicture(int id)
        {
            return context.Pictures.Find(id);
        }

        public List<Picture> GetPicturesForFolder(User user, string folderName, int counter)
        {
            try
            {
                List<Picture> pictures = context.Pictures.Where(a => a.Owner == user).ToList();
                List<Picture> picturesForSend = new List<Picture>();
               
                return picturesForSend;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string GetPictureType(int Id)
        {
            var picture = context.Pictures.FirstOrDefault(item => item.Id == Id);
            if (picture == null) { return null; }
            return picture.Type;
        }

        public Picture SavePicture(Picture picture)
        {
            context.Pictures.Add(picture);
            context.SaveChanges();
            return picture;
        }

        public bool SavePicturePath(int Id, string path)
        {
            var picture = context.Pictures.FirstOrDefault(item => item.Id == Id);
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
