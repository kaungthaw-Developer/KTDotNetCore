using KTDotNetCore.ConsoleApp.Model;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTDotNetCore.ConsoleApp.RestClientExamples
{
    public class RestClientExamples
    {
        public async Task Run()
        {
            //await Read();
            //await Edit(31);
            //await Create("Title","Autor","Context");
            //await Delete(37);
            await Update(30, "Title", "Author", "COntext");
        }

        public async Task Read()
        {
                RestRequest request = new RestRequest("https://localhost:7206/api/blog", Method.Get);
                RestClient client = new RestClient();
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content;
                var lst = JsonConvert.DeserializeObject<BlogListResponseModel>(json);
                foreach (var item in lst.Data)
                {
                    Console.WriteLine(item.Blog_Author);
                    Console.WriteLine(item.Blog_Content);
                    Console.WriteLine(item.Blog_Title);
                }
            }
        }

        private async Task Edit(int id)
        {
            RestRequest request = new RestRequest($"https://localhost:7206/api/blog/{id}", Method.Get);
            RestClient client = new RestClient();
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content;
                var model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                var item = model!.Data;
                Console.WriteLine(item.Blog_Id);
                Console.WriteLine(item.Blog_Title);
                Console.WriteLine(item.Blog_Author);
                Console.WriteLine(item.Blog_Content);
            }
            else
            {
                string jsonStr = response.Content;
                var model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                Console.WriteLine(model.Message);
            }
        }

        private async Task Create(string title, string author, string content)
        {

            BlogDataModels blog = new BlogDataModels
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };
            RestRequest request = new RestRequest("https://localhost:7206/api/blog", Method.Post);
            RestClient client = new RestClient();
            var response = await client.ExecuteAsync(request);
            
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content;
                var model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                await Console.Out.WriteLineAsync(model.Message);
            }
        }

        private async Task Update(int id, string title, string author, string content)
        {
            BlogDataModels blog = new BlogDataModels
            {
                Blog_Title = title,
                Blog_Author = author,
                Blog_Content = content
            };
            RestRequest request = new RestRequest($"https://localhost:7206/api/blog/{id}", Method.Put);
            RestClient client = new RestClient();
            var response = await client.ExecuteAsync(request);
           if(response.IsSuccessStatusCode)
            {
                string jsonStr =  response.Content!;
                var model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                await Console.Out.WriteLineAsync(model.Message);
            }
            else
            {
                string jsonStr =  response.Content!;
                var model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                Console.WriteLine(model.Message);
            }
        }

        private async Task Delete(int id)
        {
            RestRequest request = new RestRequest($"https://localhost:7206/api/blog/{id}", Method.Delete);
            RestClient client = new RestClient();
            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                var model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                Console.WriteLine(model.Message);
            }
            else
            {
                string jsonStr = response.Content!;
                var model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr);
                Console.WriteLine(model.Message);
            }
        }
    }
}
