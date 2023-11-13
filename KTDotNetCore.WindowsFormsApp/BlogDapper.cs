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
    public partial class BlogDapper : Form
    {

        private readonly SqlConnection _sqlConnection;
        public BlogDapper()
        {
            InitializeComponent();
            AppConfigService appConfigService = new AppConfigService();
            _sqlConnection = new SqlConnection(appConfigService.GetDbConnection());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = @"insert into [dbo].[tbl_blog] (
	                        [Blog_Title],
                        	[Blog_Author],
	                        [Blog_Content]) values (@Blog_Title,@Blog_Author,@Blog_Content)";
            BlogDataModels model = new BlogDataModels()
            {
                Blog_Title = textTitle.Text,
                Blog_Author = textAuthor.Text,
                Blog_Content = textContent.Text
            };
            using (IDbConnection db = new SqlConnection(_sqlConnection.ConnectionString))
            {

            db.Open();
            int suOrFa = db.Execute(query, model);
            string message = suOrFa > 0 ? "Succefull insert" : "something is wrong";
            db.Close();
            }
        }
    }
}
