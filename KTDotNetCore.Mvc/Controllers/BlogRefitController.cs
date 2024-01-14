using KTDotNetCore.Mvc.Intrerface;
using KTDotNetCore.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace KTDotNetCore.Mvc.Controllers
{
    public class BlogRefitController : Controller
    {
        private readonly IBlogApi _blogApi;

        public BlogRefitController(IBlogApi blogApi)
        {
            _blogApi = blogApi;
        }

        public async Task<IActionResult> Index()
        {
           var model=await _blogApi.GetBlogs();
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
        return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(BlogDataModels blog)
        {
            try
            {
                BlogResponseModel model = await _blogApi.CreateBlog(blog);
                TempData["Message"] = "Saving Successful.";
                TempData["IsSuccess"] = true;
                return Redirect("BlogRefit");
            }
            catch (ValidationApiException ex)
            {
                // Log details of the request and response
                Console.WriteLine(ex.StatusCode);
                Console.WriteLine($"Response: {ex}");

                // Handle the exception or rethrow it based on your needs
                throw;
            }
           
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _blogApi.GetBlog(id);
            if (!model.IsSuccess)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "No data found.";
                return Redirect("/BlogRefit");
            }
           
            var item = model!.Data;
            if (item == null)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "No data found.";
                return Redirect("/BlogRefit");
            }
            return View("Edit", item);
        }

        [HttpPost]
         public async Task<IActionResult> Update(int id, BlogDataModels reqModel)
        {
            var model = await _blogApi.GetBlog(id);
            if (!model.IsSuccess)
            {
                TempData["Message"] = "No data found.";
                TempData["IsSuccess"] = false;
                return Redirect("/BlogRefit");
            }

            var item = model!.Data;
            if (item is null)
            {
                TempData["Message"] = "No data found.";
                TempData["IsSuccess"] = false;
                return Redirect("/BlogRefit");
            }

            BlogResponseModel blog = await _blogApi.UpdateBlogs(id, reqModel);
            if(blog.IsSuccess)
            {
                TempData["Message"] = "Updating Successful.";
                TempData["IsSuccess"] = true;
            }
            return Redirect("/BlogRefit");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _blogApi.GetBlog(id);
            if (!model.IsSuccess)
            {
                TempData["Message"] = "No data found.";
                TempData["IsSuccess"] = false;
                return Redirect("/BlogRefit");
            }

            var item = model!.Data;
            if (item is null)
            {
                TempData["Message"] = "No data found.";
                TempData["IsSuccess"] = false;
                return Redirect("/BlogRefit");
            }

            BlogResponseModel blog = await _blogApi.DeleteBlog(id);

            if (blog.IsSuccess)
            {
                TempData["Message"] = "Deleting Successful.";
                TempData["IsSuccess"] = true;
            }

            return Redirect("/BlogRefit");
        }
    }
}
