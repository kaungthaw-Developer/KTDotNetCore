using Dapper;
using KTDotNetCore.RestApi.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace KTDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        private readonly SqlConnectionStringBuilder _sqlStringBuilder;
        public BlogDapperController(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DbConnection");
            _sqlStringBuilder = new SqlConnectionStringBuilder(connectionString);
        }

        [HttpGet("{id}")]
        public IActionResult Get()
        {
            string query = "select * from tbl_blog ";
            using   IDbConnection db = new SqlConnection(_sqlStringBuilder.ConnectionString);
            List<BlogDataModels> lst = db.Query<BlogDataModels>(query).ToList();
            BlogListResponseModel model = new BlogListResponseModel()
            {
                IsSuccess = true,
                Message = "SuccessFull",
                Data = lst
            };

            return Ok(model);
        }
        [HttpPost]
        public IActionResult Create([FromBody] BlogDataModels blog)
        {
            string query = @"insert into [dbo].[tbl_blog] (
	                        [Blog_Title],
                        	[Blog_Author],
	                        [Blog_Content]) values (@Blog_Title,@Blog_Author,@Blog_Content)";

            using IDbConnection db = new SqlConnection(_sqlStringBuilder.ConnectionString);
            var result = db.Execute(query, blog);
            string message = result > 0 ? "Saving Successful." : "Saving Failed.";

            BlogResponseModel model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = message,
            };
            return Ok(model);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            string query = "delete from [dbo].[tbl_blog] where Blog_Id=@Blog_Id ";
            using IDbConnection db = new SqlConnection(_sqlStringBuilder.ConnectionString);
            db.Open();
            BlogDataModels data = new BlogDataModels()
            {
                Blog_Id = id
            };
            int result = db.Execute(query, data);
            string message = result > 0 ? "succeful delete  " : "someting wrong";
            BlogResponseModel model = new BlogResponseModel();
            if (result > 0)
            {
                model.IsSuccess = result > 0;
                model.Message = message;
                return Ok(model);
            }
            model.IsSuccess = result > 0;
            model.Message = message;
            return Ok(model);
        }
        [HttpPatch("{id}")]
        public IActionResult Patch(int id)
        {
            string query = "select * from dbo.tbl_blog where Blog_Id=@Blog_Id";
            BlogDataModels blog = new BlogDataModels()
            {
                Blog_Id = id
            };
            IDbConnection db = new SqlConnection(_sqlStringBuilder.ConnectionString);
            db.Open();
            BlogDataModels item = db.Query<BlogDataModels>(query, blog).FirstOrDefault();
            BlogResponseModel model=new BlogResponseModel();
            if (item == null)
            {
                model.IsSuccess = false;
                model.Message = "data not found";
                return NotFound(model);;
            }
            string query_1 = @"update [dbo].[tbl_blog] set [Blog_Title]=@TiTlE
                              where Blog_Id=@Blog_Id";
            if (!string.IsNullOrWhiteSpace(blog.Blog_Title))
            {
                item.Blog_Title = blog.Blog_Title;
            }
            if (!string.IsNullOrWhiteSpace(blog.Blog_Author))
            {
                item.Blog_Author = blog.Blog_Author;
            }
            if (!string.IsNullOrWhiteSpace(blog.Blog_Content))
            {
                item.Blog_Content = blog.Blog_Content;
            }

            var result = db.Execute(query_1, item);

            string message = result > 0 ? "Success Update." : "something wrong.";

            model.IsSuccess = result > 0;
            model.Message = message;
            return Ok(model);
        }
    }

}
