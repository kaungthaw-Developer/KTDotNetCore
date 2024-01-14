using KTDotNetCore.MinimalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KTDotNetCore.MinimalApi
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<BlogDataModels> Blogs { get; set; }
    }
}
