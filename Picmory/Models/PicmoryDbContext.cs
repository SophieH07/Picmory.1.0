using Microsoft.EntityFrameworkCore;

namespace Picmory.Models
{
    public class PicmoryDbContext : DbContext
    {
        public PicmoryDbContext(DbContextOptions<PicmoryDbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Folder> Folders { get; set; }
    }
}
