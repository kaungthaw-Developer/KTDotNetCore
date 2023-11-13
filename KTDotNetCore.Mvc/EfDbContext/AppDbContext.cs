
using KTDotNetCore.Mvc.Model;
using Microsoft.EntityFrameworkCore;

namespace KTDotNetCore.Mvc.EfDbContext
{
    public class AppDbContext : DbContext
     {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }
        public DbSet<BlogDataModels> Blogs { get; set; }
    }
}
