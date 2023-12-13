using AB104APIIntro.Entities;
using Microsoft.EntityFrameworkCore;

namespace AB104APIIntro.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt):base(opt)
        {
            
        }

        public DbSet<Category> Categories{ get; set; }
        public DbSet<Tag> Tags{ get; set; }

    }
}
