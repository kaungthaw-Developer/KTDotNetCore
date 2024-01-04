using KTDotNetCore.ATMWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace KTDotNetCore.ATMWebApp.EFDbContext
{
    public class ATMDbContext : DbContext
    {
        public ATMDbContext(DbContextOptions<ATMDbContext> options) : base(options)
        {
        }
        public DbSet<UserDataModel> UserIfo { get; set; }
        public DbSet<UserHistoryModel> History { get; set; }
    }
}
