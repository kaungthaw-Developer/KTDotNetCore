
using KTDotNetCore.loginWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace KTDotNetCore.loginWebApp.EFDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<UserDataModel> UserIfo { get; set; }
    }
}
