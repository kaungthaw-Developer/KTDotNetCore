using Dapper;
using KTDotNetCore.WindowsFormsApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KTDotNetCore.WindowsFormsApp
{
    public partial class BlogEf : Form
    {
        private readonly AppDbContext _context;
        private readonly SqlConnection _sqlConnection;
        public BlogEf()
        {
            InitializeComponent();
            AppConfigService appConfigService= new AppConfigService();  
            _context = new AppDbContext(appConfigService.GetDbConnection());
            _sqlConnection = new SqlConnection(appConfigService.GetDbConnection());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           BlogDataModels blog=new BlogDataModels()
           {
               Blog_Author=textAuthor.Text,
               Blog_Content=textContent.Text,   
               Blog_Title=textTitle.Text,   
           };
            //_context.Blogs.Add(blog);
            //var result= _context.SaveChanges();
            //string message = result > 0 ? "succeful Create  " : "someting wrong";
            //MessageBox.Show(message, "Alert", MessageBoxButtons.OK, result > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            string query = @"insert into [dbo].[tbl_blog] (
	                        [Blog_Title],
                        	[Blog_Author],
	                        [Blog_Content]) values (@Blog_Title,@Blog_Author,@Blog_Content)";
            using (IDbConnection db = new SqlConnection(_sqlConnection.ConnectionString))
            {
                var result = db.Execute(query, blog);
                string message = result > 0 ? "Saving Successful." : "Saving Failed.";
                MessageBox.Show(message, "Alert", MessageBoxButtons.OK, result > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            }

            textAuthor.Clear();
            textContent.Clear();
            textTitle.Clear();
            textTitle.Focus();
        }
    }
}
