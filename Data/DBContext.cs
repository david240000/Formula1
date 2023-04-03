using Formula1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Data
{
    public class DBContext: IdentityDbContext<User>
    {
        public DBContext(DbContextOptions<DBContext> options):base(options)
        {
        
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Image> Image { get; set; }
    }
}
