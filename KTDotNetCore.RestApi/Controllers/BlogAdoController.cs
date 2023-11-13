using KTDotNetCore.RestApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.Metadata;

namespace KTDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoController : ControllerBase
    {
        private readonly SqlConnectionStringBuilder _sqlStringBuilder;
        public BlogAdoController(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DbConnection");
            _sqlStringBuilder = new SqlConnectionStringBuilder(connectionString);
        }
        [HttpGet]
        public IActionResult Get()
        {
            string query = "select * from tbl_blog";

            SqlConnection connection = new SqlConnection(_sqlStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            List<BlogDataModels> lst = new List<BlogDataModels>();
            foreach (DataRow dr in dt.Rows)
            {
                BlogDataModels item = new BlogDataModels
                {
                    Blog_Id = Convert.ToInt32(dr["Blog_Id"]),
                    Blog_Title = dr["Blog_Title"].ToString(),
                    Blog_Author = dr["Blog_Author"].ToString(),
                    Blog_Content = dr["Blog_Content"].ToString(),
                };
                lst.Add(item);
            }
            BlogListResponseModel model = new BlogListResponseModel()
            {
                IsSuccess = true,
                Message = "Success",
                Data = lst
            };

            return Ok(model);
        }

        [HttpPost]
        public IActionResult Create([FromBody] BlogDataModels model)
        {
            string query = @"insert into [dbo].[tbl_blog] (
	                        [Blog_Title],
                        	[Blog_Author],
	                        [Blog_Content]) values (@title,@author,@content)";
            SqlConnection connection = new SqlConnection(_sqlStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("title", model.Blog_Title);
            cmd.Parameters.AddWithValue("Author", model.Blog_Author);
            cmd.Parameters.AddWithValue("content", model.Blog_Content);
            int result = cmd.ExecuteNonQuery();
            string message = result > 0 ? "Successfull entry" : "no somone have error";
            connection.Close();

            BlogResponseModel models = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = message,
            };
            return Ok(models);
        }
        [HttpGet("{id}")]
        public IActionResult Delete(int id)
        {
            string query = @"delete from [dbo].[tbl_blog]
                                where Blog_Id=@Id";
            SqlConnection connection = new SqlConnection(_sqlStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("id", id);

            int result = cmd.ExecuteNonQuery();
            string message = result > 0 ? "Successfull delete" : "no somone have error";
            connection.Close();
            BlogResponseModel models = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = message,
            };
            return Ok(models);
        }
        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            string query = "select * from dbo.tbl_blog where Blog_Id=@Blog_Id";
            SqlConnection connection = new SqlConnection(_sqlStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("Blog_Id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();
            BlogResponseModel models = new BlogResponseModel();
            if (dt.Rows.Count == 0)
            {
                models.IsSuccess = false;
                models.Message = "No Data Found!!";
                return NotFound(models);
            }
            DataRow dr = dt.Rows[0];
            BlogDataModels dataModel = new BlogDataModels()
            {
                Blog_Id = Convert.ToInt32(dr["Blog_Id"]),
                Blog_Title = dr["Blog_Title"].ToString(),
                Blog_Author = dr["Blog_Author"].ToString(),
                Blog_Content = dr["Blog_Content"].ToString()
            };
            models.IsSuccess = true;
            models.Message = "Success";
            models.Data = dataModel;
            return Ok(models);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody] BlogDataModels model)
        {
            string query = @"update [dbo].[tbl_blog] set [Blog_Title]=@Blog_Title,[Blog_Author] = @Blog_Author,
                             [Blog_Content] = @Blog_Content
                              where Blog_Id=@Id";
            SqlConnection connection = new SqlConnection(_sqlStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("id", id);
            cmd.Parameters.AddWithValue("Blog_Title", model.Blog_Title);
            cmd.Parameters.AddWithValue("Blog_Content", model.Blog_Content);
            cmd.Parameters.AddWithValue("Blog_Author", model.Blog_Author);

            int result = cmd.ExecuteNonQuery();
            string message = result > 0 ? "Successfull update" : "no somone have error";
            connection.Close();
            BlogResponseModel blog = new BlogResponseModel();
            blog.IsSuccess = result > 0;
            blog.Message = message;
            if (result < 0)
            {
                return NotFound(blog);
            }
            model.Blog_Id = id;
            blog.Data = model;
            return Ok(blog);
        }
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] BlogViewModels blog)
        {
            string query = "select * from dbo.tbl_blog where Blog_Id=@Blog_Id";
            SqlConnection connection = new SqlConnection(_sqlStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            BlogResponseModel responseModle = new BlogResponseModel();
            if (dt.Rows.Count == 0)
            {
                responseModle.IsSuccess = false;
                responseModle.Message = "No Data Found!!";
                return NotFound(responseModle);
            }
            DataRow dr = dt.Rows[0];
            BlogDataModels model = new BlogDataModels()
            {
                Blog_Id = Convert.ToInt32(dr["Blog_Id"]),
                Blog_Title = dr["Blog_Title"].ToString(),
                Blog_Author = dr["Blog_Author"].ToString(),
                Blog_Content = dr["Blog_Content"].ToString()
            };

            string query1 = @"UPDATE [dbo].[tbl_Blog]
                             SET
                             [Blog_Title] = @Blog_Title,
                             [Blog_Author] = @Blog_Author,
                             [Blog_Content] = @Blog_Content
                             WHERE
                             [Blog_Id] = @Blog_Id";

            SqlCommand cmd1 = new SqlCommand(query1, connection);

            cmd1.Parameters.AddWithValue("@Blog_Id", id);

            if (!string.IsNullOrWhiteSpace(blog.Title))
            {
                cmd1.Parameters.AddWithValue("@Blog_Title", blog.Title);
            }
            else
            {
                cmd1.Parameters.AddWithValue("@Blog_Title", model.Blog_Title);
            }

            if (!string.IsNullOrWhiteSpace(blog.Author))
            {
                cmd1.Parameters.AddWithValue("@Blog_Author", blog.Author);
            }
            else
            {
                cmd1.Parameters.AddWithValue("@Blog_Author", model.Blog_Author);
            }

            if (!string.IsNullOrWhiteSpace(blog.Blog_Content))
            {
                cmd1.Parameters.AddWithValue("@Blog_Content", blog.Blog_Content);
            }
            else
            {
                cmd1.Parameters.AddWithValue("@Blog_Content", model.Blog_Content);
            }

            int result = cmd1.ExecuteNonQuery();

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";

            responseModle.IsSuccess = result > 0;
            responseModle.Message = message;
            return Ok(responseModle);
        }
    }
}
