using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Dapper;
using System.Linq;
using KTDotNetCore.ConsoleApp.Model;

namespace KTDotNetCore.ConsoleApp.DapperExamples
{
    public class DapperExample
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
            Read();
        }
        public void Read()
        {
            string query = "select * from tbl_blog ";
            using IDbConnection db = new SqlConnection(connectionStringBuiders.ConnectionString);
            List<BlogDataModels> lst=db.Query<BlogDataModels>(query).ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);

            }
            //var list=db.Query(query).ToList();
            //foreach (var item in list)
            //{
            //    Console.WriteLine(item.Blog_Id);
            //    Console.WriteLine(item.Blog_Title);  
            //    Console.WriteLine(item.Blog_Author);    
            //    Console.WriteLine(item.Blog_Content);

            //}
        }
        public void created(string title, string author, string content)
        {
            string query = @"insert into [dbo].[tbl_blog] (
	                        [Blog_Title],
                        	[Blog_Author],
	                        [Blog_Content]) values (@Blog_Title,@Blog_Author,@Blog_Content)";
            using IDbConnection db=new SqlConnection(connectionStringBuiders.ConnectionString);
            db.Open();
            BlogDataModels model = new BlogDataModels()
            {
                Blog_Title =title,
                Blog_Author = author,
                Blog_Content = content
            };
            int suOrFa=db.Execute(query,model);
            string message = suOrFa > 0 ? "Succefull insert" : "something is wrong";
          db.Close();
            Console.WriteLine(message);
            //SqlConnection connection = new SqlConnection(connectionStringBuiders.ConnectionString);
            //connection.Open();
            //SqlCommand cmd = new SqlCommand(query, connection);
            //cmd.Parameters.AddWithValue("title", title);
            //cmd.Parameters.AddWithValue("Author", author);
            //cmd.Parameters.AddWithValue("content", content);
            //int result = cmd.ExecuteNonQuery();
            //string message = result > 0 ? "Successfull entry" : "no somone have error";
            //connection.Close();
            //Console.WriteLine(message);

        }
        public void Delete(string id)
        {
            string query= "delete from [dbo].[tbl_blog] where Blog_Id=@ID ";
            // string query= "delete from [dbo].[tbl_blog] where Blog_Id=@Blog_Id ";
            using IDbConnection db = new SqlConnection(connectionStringBuiders.ConnectionString);
            db.Open();
            //BlogDataModels data = new BlogDataModels()
            //{
            //    Blog_Id = ID
            //};
            int result = db.Execute(query,new { id }); //another way
          //  int result = db.Execute(query,data); //another way
            string message = result > 0 ? "succeful delete  " : "someting wrong";
            db.Close();
            Console.WriteLine(message); 
        }
        public void Edit(int id)
        {
                string query = "select * from dbo.tbl_blog where Blog_Id=@Blog_Id";
                BlogDataModels blog = new BlogDataModels()
                {
                    Blog_Id = id
                };
                IDbConnection db = new SqlConnection(connectionStringBuiders.ConnectionString);
                db.Open();
            BlogDataModels item=db.Query<BlogDataModels>(query,blog).FirstOrDefault();
            if (item == null)
            {
                Console.WriteLine("Not Data found");
                db.Close();
                return;
            }
            else
            {
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
                Console.WriteLine(item.Blog_Title);
            }
        }
        public void Update(int id, string title)
        {
            string query = @"update [dbo].[tbl_blog] set [Blog_Title]=@TiTlE
                              where Blog_Id=@ID";
            //string query = @"update [dbo].[tbl_blog] set [Blog_Title]=@Blog_Id
            //                  where Blog_Id=@ID";
            using IDbConnection db=new SqlConnection(connectionStringBuiders.ConnectionString);  
            db.Open();
            BlogDataModels model=new BlogDataModels()
            {
                Blog_Id=id,
                Blog_Title=title,
            };

            // int result = db.Execute(query,model);// another way
            int result = db.Execute(query,new {id,title});
            string message = result > 0 ? "Successfull update" : "no somone have error";
            db.Close();
            Console.WriteLine(message);

        }
    }
}
