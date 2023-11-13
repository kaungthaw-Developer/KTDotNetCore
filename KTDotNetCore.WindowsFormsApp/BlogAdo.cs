using KTDotNetCore.WindowsFormsApp;
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

namespace AHMTZDotNetCore.WindowsFormsApp
{
    public partial class BlogAdo : Form
    {
        private readonly SqlConnection _sqlConnection;
        public BlogAdo()
        {
            InitializeComponent();
            AppConfigService appConfigService = new AppConfigService();
            _sqlConnection = new SqlConnection(appConfigService.GetDbConnection());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_Content])
     VALUES
           (@Blog_Title
           ,@Blog_Author
           ,@Blog_Content)";

            SqlConnection connection = new SqlConnection(_sqlConnection.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Title", textBox1.Text);
            cmd.Parameters.AddWithValue("@Blog_Author", textBox2.Text);
            cmd.Parameters.AddWithValue("@Blog_Content", textBox3.Text);
            int result = cmd.ExecuteNonQuery();
            string message = result > 0 ? "Saving Successful." : "Saving Failed.";

            connection.Close();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox1.Focus();
        }

        private void BlogAdo_Load(object sender, EventArgs e)
        {

        }
    }
}
