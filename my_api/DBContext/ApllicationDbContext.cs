using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using my_api.IdentityAuth;


namespace my_api.DBContext
{
    public class ApllicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApllicationDbContext(DbContextOptions<ApllicationDbContext> options): base(options)
        {

        }
       public DbSet<ApplicationUser> applicationUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser { Name = "test1", PassWord = "12345" },
                 new ApplicationUser { Name = "test2", PassWord = "12345" }
                );
           
            base.OnModelCreating(modelBuilder);
        }

    }
}
