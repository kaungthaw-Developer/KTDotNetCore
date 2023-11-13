using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;

namespace KTDotNetCore.ConsoleApp.AdoDotNetExamples
{
    public class AdoDotNetExample
    {
     private readonly SqlConnectionStringBuilder connectionStringBuiders = new SqlConnectionStringBuilder()
        {
            DataSource = "LAPTOP-722Q22P3",
            InitialCatalog = "KTDotNetCore",
            UserID = "sa",
            Password = "sasa"

        };
        public void Run()
        {
            Read(1);
        }
        public void Read(int pageNo, int pageSize = 5)
        {
            int skipRowCount = (pageNo - 1) * pageSize;
            string query = @"select * from tbl_blog order by Blog_Id desc OFFSET
@SkipRowCount ROWS FETCH NEXT @PageSize ROWS ONLY";
            SqlConnection connection = new SqlConnection(connectionStringBuiders.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query,connection);
            cmd.Parameters.AddWithValue("@SkipRowCount", skipRowCount);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt=new DataTable(); 
            adapter.Fill(dt);
            connection.Close();
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["Blog_Id"].ToString());
                Console.WriteLine(dr["Blog_Author"].ToString());
                Console.WriteLine(dr["Blog_Title"].ToString());
                Console.WriteLine(dr["Blog_Content"].ToString());

            }
        }
        public void created(string title,string author,string content)
        {
            string query = @"insert into [dbo].[tbl_blog] (
	                        [Blog_Title],
                        	[Blog_Author],
	                        [Blog_Content]) values (@title,@author,@content)";
            SqlConnection connection = new SqlConnection(connectionStringBuiders.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query,connection);
            cmd.Parameters.AddWithValue("title", title);
            cmd.Parameters.AddWithValue("Author", author);  
            cmd.Parameters.AddWithValue ("content", content);   
            int result=cmd.ExecuteNonQuery();
            string message = result > 0 ? "Successfull entry" : "no somone have error";
            connection.Close();
            Console.WriteLine(message);

        }
        public void Delete(string id)
        {
            string query = @"delete from [dbo].[tbl_blog]
                                where Blog_Id=@Id";
            SqlConnection connection = new SqlConnection(connectionStringBuiders.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("id", id);
           
            int result = cmd.ExecuteNonQuery();
            string message = result > 0 ? "Successfull delete" : "no somone have error";
            connection.Close();
            Console.WriteLine(message);

        }
        public void Edit(string id) 
        {
            string query = "select * from dbo.tbl_blog where Blog_Id=@Blog_Id";
            SqlConnection connection = new SqlConnection( connectionStringBuiders.ConnectionString);
            connection.Open();
            SqlCommand cmd=new SqlCommand(query,connection);
            cmd.Parameters.AddWithValue("Blog_Id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("Not data found");
            }
            else
            {
                DataRow dr = dt.Rows[0];
                Console.WriteLine(dr["Blog_Id"].ToString());
                Console.WriteLine(dr["Blog_Title"].ToString());
                Console.WriteLine(dr["Blog_Author"].ToString());
                Console.WriteLine(dr["Blog_Content"].ToString());
            }

        }
        public void Update(string id,string title)
        {
            string query = @"update [dbo].[tbl_blog] set [Blog_Title]=@Title
                              where Blog_Id=@Id";
            SqlConnection connection = new SqlConnection(connectionStringBuiders.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("id", id);
            cmd.Parameters.AddWithValue("title", title);

            int result = cmd.ExecuteNonQuery();
            string message = result > 0 ? "Successfull update" : "no somone have error";
            connection.Close();
            Console.WriteLine(message);

        }

    }
}
