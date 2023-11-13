using KTDotNetCore.RestApi.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Text;

namespace KTDotNetCore.RestApi.EFCoreExamples
{
    public class AppDbContext:DbContext
    {
        private readonly SqlConnectionStringBuilder connectionStringBuiders = new SqlConnectionStringBuilder()
        {
            DataSource = "LAPTOP-722Q22P3",
            InitialCatalog = "KTDotNetCore",
            UserID = "sa",
            Password = "sasa"

        };
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionStringBuiders.ConnectionString);
            }
        }

        public DbSet<BlogDataModels> Blogs { get; set; }

    }
}
