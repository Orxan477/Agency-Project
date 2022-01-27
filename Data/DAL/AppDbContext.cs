using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.DAL
{
   public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
        }
        
            public DbSet<Portfolio> Portfolio { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Portfolio>().HasData(
                new Portfolio { Id = 1, Name= "Threads", Image= "1.jpg", Info= "Use this area to describe your project.", 
                                                                                            Category= "Illustration", Client="Threads" }

                );
            base.OnModelCreating(builder);
        }
    }
}
