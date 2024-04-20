using Microsoft.EntityFrameworkCore;
using SERWalks.API.Models.Domain;

namespace SERWalks.API.Data
{
    public class SERWalksDbContext : DbContext
    {
        public SERWalksDbContext(DbContextOptions<SERWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed data for Difficulties
            //Easy, Medium, Hard

            var difficulties = new List<Difficulty>()
            {
                new Difficulty
                {
                    Id= Guid.Parse("7734507a-1505-46bd-8416-989cbff2fe19"),
                    Name="Easy"
                },new Difficulty
                {
                    Id= Guid.Parse("819bbef6-a149-4390-9e99-589b9dcacd49"),
                    Name="Medium"
                },new Difficulty
                {
                    Id= Guid.Parse("9cc663aa-3ff3-439a-a5f3-0e733272b1b7"),
                    Name="Hard"
                }
            };

            //Seed difficulties to the databse
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //Seed data for regions
            var regions = new List<Region>
            {
                new Region()
                {
                    Id=Guid.Parse("757ec18b-c924-408e-9456-5587dcaab8a4"),
                    Code="SUM",
                    Name="Sumadija",
                    RegionImageUrl="sumadija.jpg"
                },new Region()
                {
                    Id=Guid.Parse("38d42c0c-aeac-425b-a1f7-f942708ef693"),
                    Code="VOJ",
                    Name="Vojvodina",
                    RegionImageUrl="vojvodina.jpg"
                },new Region()
                {
                    Id=Guid.Parse("309b7bc9-339d-4855-aa85-573cef4ceb50"),
                    Code="POM",
                    Name="Pomoravlje",
                    RegionImageUrl="pomoravlje.jpg"
                },new Region()
                {
                    Id=Guid.Parse("c41ed9d3-5dc7-4a94-84cc-0be563500b61"),
                    Code="BEO",
                    Name="Beograd",
                    RegionImageUrl="beograd.jpg"
                }
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
