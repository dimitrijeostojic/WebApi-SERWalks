using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SERWalks.API.Data
{
    public class SERWalksAuthDbContext : IdentityDbContext
    {
        public SERWalksAuthDbContext(DbContextOptions<SERWalksAuthDbContext> options) : base(options)
        {

        }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "aaf7295e-0576-4520-b9cb-5981fa8b4f49";
            var writerRoleId = "1fc9a9b7-77ff-4836-85b9-67fb7dfd2768";

            var roles = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Id=readerRoleId,
                    ConcurrencyStamp=readerRoleId,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper()
                },
                new IdentityRole()
                {
                    Id=writerRoleId,
                    ConcurrencyStamp=writerRoleId,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
