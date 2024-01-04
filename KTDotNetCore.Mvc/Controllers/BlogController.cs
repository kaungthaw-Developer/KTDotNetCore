using KTDotNetCore.Mvc.EfDbContext;
using KTDotNetCore.Mvc.Model;
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
            BlogListResponseModel model = new BlogListResponseModel
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

       


      
    }
}
