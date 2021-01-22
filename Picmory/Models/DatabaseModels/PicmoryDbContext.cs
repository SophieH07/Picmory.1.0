using Microsoft.EntityFrameworkCore;
using Picmory.Models.DatabaseModels;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Picmory.Models
{
    public class PicmoryDbContext : DbContext
    {
        public override int SaveChanges()
        {
            SetProperties();
            return base.SaveChanges();
        }


        public PicmoryDbContext(DbContextOptions<PicmoryDbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<Like> Likes { get; set; }

        private void SetProperties()
        {
            foreach (var entity in ChangeTracker.Entries().Where(p => p.State == EntityState.Added))
            {
                var created = entity.Entity as IDateCreated;
                if (created != null)
                {
                    created.DateCreated = DateTime.Now;
                }
            }

            
        }
    }
}
