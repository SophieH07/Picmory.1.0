using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Models.Repositorys
{
    public interface IPictureRepository
    {
        public Picture SavePicture(Picture picture);
        public bool SavePicturePath(User user, int Id, string path);
    }
}
