using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picmory.Models
{
    public class PicmoryDbContext : DbContext
    {
        public PicmoryDbContext(DbContextOptions<PicmoryDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
