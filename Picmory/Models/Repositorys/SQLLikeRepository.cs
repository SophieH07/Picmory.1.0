using Picmory.Models.Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Models.Repositorys
{
    public class SQLLikeRepository : ILikeRepository
    {
        private PicmoryDbContext context;
        public SQLLikeRepository(PicmoryDbContext context)
        {
            this.context = context;
        }


        public List<string> ListOfLikers(int pictureId)
        {
            return context.Likes.Where(a => a.Picture.Id == pictureId).Select(a => a.Owner.UserName).ToList();
        }

        public bool SaveDislike(int pictureId, User likerUser)
        {
            if (context.Likes.Where(a => a.Owner == likerUser && a.Picture.Id == pictureId).SingleOrDefault() != null)
            {
                context.Likes.Remove(context.Likes.Where(a => a.Owner == likerUser && a.Picture.Id == pictureId).SingleOrDefault());
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool SaveLike(int pictureId, User likerUser)
        {
            if (context.Likes.Where(a => a.Owner == likerUser && a.Picture.Id == pictureId).SingleOrDefault() == null)
            {
                context.Likes.Add(new Like(likerUser, context.Pictures.Where(a => a.Id == pictureId).SingleOrDefault()));
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
