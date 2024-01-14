using KTDotNetCore.Mvc.EfDbContext;
using KTDotNetCore.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KTDotNetCore.Mvc.Controllers
{
    public class BlogController : Controller
    { 
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context=context;
        }
        public async Task<IActionResult> Index(int pageNo = 1, int pageSize = 10)
        {
            List<BlogDataModels> lst = await _context.Blogs.AsNoTracking().OrderByDescending(x => x.Blog_Id)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            int pageRowCount = await _context.Blogs.CountAsync();
            int pageCount = pageRowCount / pageSize;
            if (pageRowCount % pageSize > 0)
                pageCount++;
            BlogModel model = new BlogModel
            {
                BlogList = lst,
                PageCount = pageCount,
                PageNo = pageNo,
                PageRowCount = pageRowCount,
                PageSize = pageSize
            };

            //throw new Exception("heehee");

            return View("BlogIndex", model);
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> Save(BlogDataModels blog)
        {
            await _context.AddAsync(blog);
            var result = await _context.SaveChangesAsync();
            TempData["IsSuccess"] = result > 0;
            TempData["Message"] = result > 0 ? "Success  save" : "something was wrong.";
            return Redirect("/Blog");
        }

        [ActionName("Create")]
        public IActionResult create()
        {
          
            return View("create");
        }

        [ActionName("Edit")]
        public async Task<IActionResult> BlogEdit(int id)
        {
            bool isExist = await _context.Blogs.AsNoTracking().AnyAsync(x => x.Blog_Id == id);
            if (!isExist)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "No data found.";
                return Redirect("/Blog");
            }
            var item = await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(x => x.Blog_Id == id);
            if (item == null)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "No data found.";
                return Redirect("/Blog");
            }
            return View("BlogEdit", item);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> BlogUpdate(int id, BlogDataModels reqModel)
        {
            if (!await _context.Blogs.AsNoTracking().AnyAsync(x => x.Blog_Id == id))
            {
                TempData["Message"] = "No data found.";
                TempData["IsSuccess"] = false;
                return Redirect("/blog");
            }

            var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.Blog_Id == id);
            if (blog is null)
            {
                TempData["Message"] = "No data found.";
                TempData["IsSuccess"] = false;
                return Redirect("/blog");
            }

            blog.Blog_Title = reqModel.Blog_Title;
            blog.Blog_Author = reqModel.Blog_Author;
            blog.Blog_Content = reqModel.Blog_Content;

            int result = _context.SaveChanges();
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            TempData["Message"] = message;
            TempData["IsSuccess"] = result > 0;

            return Redirect("/blog");
        }

        [ActionName("Delete")]
        public async Task<IActionResult> BlogDelete(int id)
        {
            if (!await _context.Blogs.AsNoTracking().AnyAsync(x => x.Blog_Id == id))
            {
                TempData["Message"] = "No data found.";
                TempData["IsSuccess"] = false;
                return Redirect("/blog");
            }

            var blog = await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(x => x.Blog_Id == id);
            if (blog is null)
            {
                TempData["Message"] = "No data found.";
                TempData["IsSuccess"] = false;
                return Redirect("/blog");
            }

            _context.Remove(blog);
            int result = _context.SaveChanges();
            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            TempData["Message"] = message;
            TempData["IsSuccess"] = result > 0;

            return Redirect("/blog");
        }
    }
}
