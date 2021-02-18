using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Models.Repositorys.Interfaces
{
    public interface ILikeRepository
    {
        public bool SaveLike(int pictureId, User likerUser);
        public bool SaveDislike(int pictureId, User likerUser);
        public List<string> ListOfLikers(int pictureId);
    }
}
