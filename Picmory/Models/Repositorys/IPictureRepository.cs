
namespace Picmory.Models.Repositorys
{
    public interface IPictureRepository
    {
        public Picture SavePicture(Picture picture);
        public bool SavePicturePath(int Id, string path);
        public string GetPictureType(int Id);
    }
}
