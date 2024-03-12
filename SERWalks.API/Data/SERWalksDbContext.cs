using Microsoft.EntityFrameworkCore;
using SERWalks.API.Models.Domain;

namespace SERWalks.API.Data
{
    public class SERWalksDbContext : DbContext
    {
        public SERWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
