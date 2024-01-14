using KTDotNetCore.MinimalApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KTDotNetCore.MinimalApi.Feature.Blog;

public static class BlogService
{
    public static void AddBlogService(this IEndpointRouteBuilder app)
    {
        app.MapGet("/blog", async ([FromServices] AppDbContext db) =>
        {
            List<BlogDataModels> lst = db.Blogs.ToList();
            BlogListResponseModel model = new BlogListResponseModel()
            {
                IsSuccess = true,
                Message = "Success",
                Data = lst
            };
            return Results.Ok(model);

        })
        .WithName("GetBlog")
        .WithOpenApi();

        app.MapGet("/blog/{pageNo}/{pageSize}", async ([FromServices] AppDbContext db, int pageNo, int pageSize) =>
        {
            return await db.Blogs
                .AsNoTracking()
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        })
        .WithName("GetBlogs")
        .WithOpenApi();

        app.MapPost("/blog", async ([FromServices] AppDbContext db, BlogDataModels blog) =>
        {
            await db.Blogs.AddAsync(blog);
            int result = await db.SaveChangesAsync();

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            return Results.Ok(new BlogResponseModel
            {
                Data = blog,
                IsSuccess = result > 0,
                Message = message
            });
        })
       .WithName("CreateBlog")
       .WithOpenApi();

        app.MapPut("/blog", async ([FromServices] AppDbContext db, int id, BlogDataModels blog) =>
        {
            BlogResponseModel model = new BlogResponseModel();

            BlogDataModels item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);
            if (item == null)
            {
                model.IsSuccess = false;
                model.Message = "No data found.";
                return Results.NotFound(id);
            }

            item.Blog_Title = blog.Blog_Title;
            item.Blog_Author = blog.Blog_Author;
            item.Blog_Content = blog.Blog_Content;

            var result = db.SaveChanges();
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";

            model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = message,
            };
            return Results.Ok(model);
        })
       .WithName("UpdateBlog")
       .WithOpenApi();

        app.MapPatch("/blog", async ([FromServices] AppDbContext db, int id, BlogDataModels blog) =>
        {
            BlogResponseModel model = new BlogResponseModel();
            var item = db.Blogs.FirstOrDefault(x => x.Blog_Id == id);

            if (item is null)
            {
                model.IsSuccess = false;
                model.Message = "No data found.";
                return Results.NotFound(model);
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

            var result = db.SaveChanges();
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";

            model = new BlogResponseModel()
            {
                IsSuccess = result > 0,
                Message = message,
            };

            return Results.Ok(model);
        })
      .WithName("PatchBlog")
      .WithOpenApi();

        app.MapDelete("/blog", async ([FromServices] AppDbContext db, int id) =>
        {

            var blog = db.Blogs.FirstOrDefault(b => b.Blog_Id == id);

            BlogResponseModel data = new BlogResponseModel();
            if (blog is null)
            {
                data.IsSuccess = false;
                data.Message = "No data found.";
                return Results.NotFound(id);
            }

            db.Blogs.Remove(blog);
            db.SaveChanges();
            data.IsSuccess = true;
            data.Message = "Success";
            return Results.Ok(data);
        })
       .WithName("DeleteBlog")
       .WithOpenApi();

    }


        
        
}
