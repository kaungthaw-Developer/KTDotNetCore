using KTDotNetCore.RestApi;
using KTDotNetCore.RestApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KTDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult GetBlog()
        {
            List<BlogDataModels> lst = _context.Blogs.ToList();
            BlogListResponseModel model = new BlogListResponseModel()
            {
                IsSuccess = true,
                Message = "Success",
                //Data = lst.Where(x => x.Blog_Title == "").OrderByDescending(x => x.Blog_Id).ToList()
                Data = lst
            };
            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult EditBlog(int id)
        {
            BlogResponseModel model = new BlogResponseModel();

            BlogDataModels item = _context.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item is null)
            {
                model.IsSuccess = false;
                model.Message = "No data found.";
                return NotFound(model);
            }

            model.IsSuccess = true;
            model.Message = "Success";
            model.Data = item;
            return Ok(model);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogDataModels blog)
        {
            _context.Blogs.Add(blog);
            var result = _context.SaveChanges();
            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            BlogResponseModel model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = message,
            };
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogDataModels blog)
        {
            BlogResponseModel model = new BlogResponseModel();

            BlogDataModels item = _context.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item == null)
            {
                model.IsSuccess = false;
                model.Message = "No data found.";
                return NotFound(model);
            }

            item.Blog_Title = blog.Blog_Title;
            item.Blog_Author = blog.Blog_Author;
            item.Blog_Content = blog.Blog_Content;

            var result = _context.SaveChanges();
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";

            model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = message,
            };
            return Ok(model);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, [FromBody] BlogDataModels blog)
        {
            BlogResponseModel model = new BlogResponseModel();
            var item = _context.Blogs.FirstOrDefault(x => x.Blog_Id == id);

            if (item is null)
            {
                model.IsSuccess = false;
                model.Message = "No data found.";
                return NotFound(model);
            }

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

            var result = _context.SaveChanges();
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";

            model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = message,
            };

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var blog = _context.Blogs.FirstOrDefault(b => b.Blog_Id == id);

            BlogResponseModel data = new BlogResponseModel();
            if (blog is null)
            {
                data.IsSuccess = false;
                data.Message = "No data found.";
                return NotFound(data);
            }

            _context.Blogs.Remove(blog);
            _context.SaveChanges();
            data.IsSuccess = true;
            data.Message = "Success";
            return Ok(data);
        }
    }
}
