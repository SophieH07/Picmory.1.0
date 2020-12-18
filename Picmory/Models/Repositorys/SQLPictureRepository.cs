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
