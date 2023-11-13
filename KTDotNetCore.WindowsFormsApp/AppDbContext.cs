using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using KTDotNetCore.WindowsFormsApp.Model;

namespace KTDotNetCore.WindowsFormsApp
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(string nameOrConnectionString) : base(nameOrConnectionString) 
        {

        }
        public DbSet<BlogDataModels> Blogs { get; set; }
    }
}
