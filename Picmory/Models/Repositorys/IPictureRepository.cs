
using Picmory.Models.RequestModels;
using Picmory.Util;
using System.Collections.Generic;

namespace Picmory.Models.Repositorys
{
    public interface IPictureRepository
    {
        public Picture SavePicture(Picture picture);
        public bool SavePicturePath(int Id, string path);
        public string GetPictureType(int Id);
        public Picture GetPicture(int id);
        public List<Picture> GetPicturesForMe(User user, int offset, string folderName);
        Success ChangePictureData(PictureChange changeData);
        Success DeletePicture(int pictureId);
        List<Picture> GetPicturesFromOther(User user, User otherUser, int offset, string folderName);
    }
}
