
using System.Collections.Generic;

namespace Picmory.Models.Repositorys
{
    public interface IPictureRepository
    {
        public Picture SavePicture(Picture picture);
        public bool SavePicturePath(int Id, string path);
        public string GetPictureType(int Id);
        public Picture GetPicture(int id);
        public List<Picture> GetPicturesForFolder(User user, string folderName, int counter);
    }
}
