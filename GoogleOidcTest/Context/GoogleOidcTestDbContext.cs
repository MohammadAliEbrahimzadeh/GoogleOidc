using GoogleOidcTest.Configurations;
using GoogleOidcTest.Models;
using Microsoft.EntityFrameworkCore;

namespace GoogleOidcTest.Context;

public class GoogleOidcTestDbContext : DbContext
{
        public GoogleOidcTestDbContext(DbContextOptions<GoogleOidcTestDbContext> options) : base(options)
        {
                
        }


        public DbSet<User>? Users { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

}
