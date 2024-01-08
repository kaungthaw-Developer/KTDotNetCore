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

        [HttpGet]
        public IActionResult GetBlog()
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
        public IActionResult CreateBlog([FromBody] BlogDataModels blog)
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

        [HttpGet("{id}")]
        public IActionResult EditBlog(int id)
        {
            string query = "SELECT * FROM [Tbl_Blog] WHERE [Blog_Id] = @Blog_Id";

            BlogDataModels dataModel = new BlogDataModels()
            {
                Blog_Id = id,
            };

            using IDbConnection db = new SqlConnection(_sqlStringBuilder.ConnectionString);
            BlogDataModels item = db.Query<BlogDataModels>(query, dataModel).FirstOrDefault();

            BlogResponseModel model = new BlogResponseModel();
            if (item == null)
            {
                model.IsSuccess = false;
                model.Message = "No Data Found!!";
                return NotFound(model);
            }

            model.IsSuccess = true;
            model.Message = "Success";
            model.Data = item;
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, [FromBody] BlogDataModels blog)
        {
            string query = @"UPDATE [dbo].[Tbl_Blog]
                             SET
                             [Blog_Title] = @Blog_Title,
                             [Blog_Author] = @Blog_Author,
                             [Blog_Content] = @Blog_Content
                             WHERE
                             [Blog_Id] = @Blog_Id";

            using IDbConnection db = new SqlConnection(_sqlStringBuilder.ConnectionString);
            blog.Blog_Id = id;
            var result = db.Execute(query, blog);

            string message = result > 0 ? "Update Successful !!" : "Error While Update !!";

            BlogResponseModel model = new BlogResponseModel();
            model.IsSuccess = result > 0;
            model.Message = message;

            if (result < 0)
            {
                return NotFound(model);
            }
            model.Data = blog;
            return Ok(model);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id)
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

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
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
    }

}
