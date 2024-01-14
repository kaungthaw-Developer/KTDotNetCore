using KTDotNetCore.Mvc.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTDotNetCore.Mvc.Intrerface
{
    public  interface IBlogApi
    {
        [Get("/api/blog")]
        Task<BlogListResponseModel> GetBlogs();

        [Get("/api/blog/{id}")]
        Task<BlogResponseModel> GetBlog(int id);

        [Post("/api/blog")]
        Task<BlogResponseModel> CreateBlog(BlogDataModels blog);

        [Put("/api/blog/{id}")]
        Task<BlogResponseModel> UpdateBlogs(int id, BlogDataModels blog);

        [Delete("/api/blog/{id}")]
        Task<BlogResponseModel> DeleteBlog(int id);

    }
}
