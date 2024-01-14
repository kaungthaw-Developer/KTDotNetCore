using KTDotNetCore.ConsoleApp.Model;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTDotNetCore.ConsoleApp.RefitExamples
{
    public class RefitExamples
    {
        private readonly IBlogApi blogApi = RestService.For<IBlogApi>("https://localhost:7244");

        public async Task Run()
        {
            //await Create("test 1", "test 2", "test 3");
            //await Read();
            //await Edit(14);
            //await Update(18, "test 4", "test 5", "test6");
            //await Delete(14);
            await Read();
        }

        private async Task Read()
        {
            var model = await blogApi.GetBlogs();
            foreach (var item in model!.Data)
            {
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
            }
        }

        private async Task Edit(int id)
        {
            var model = await blogApi.GetBlog(id);
            var item = model!.Data;
            Console.WriteLine(item.Blog_Id);
            Console.WriteLine(item.Blog_Title);
            Console.WriteLine(item.Blog_Author);
            Console.WriteLine(item.Blog_Content);
        }

        private async Task Create(string title, string author, string content)
        {
            BlogDataModels blog = new BlogDataModels
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };
            var model = await blogApi.CreateBlog(blog);
            await Console.Out.WriteLineAsync(model.Message);
        }

        private async Task Update(int id, string title, string author, string content)
        {
            BlogResponseModel model = await blogApi.UpdateBlog(id, new BlogDataModels()
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content,
            });
            await Console.Out.WriteLineAsync(model.Message);
        }

        private async Task Delete(int id)
        {
            BlogResponseModel model = await blogApi.DeleteBlog(id);

            await Console.Out.WriteLineAsync(model.Message);
        }
    }
}
