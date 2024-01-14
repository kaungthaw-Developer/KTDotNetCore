
using Azure.Core;
using KTDotNetCore.Mvc.Intrerface;
using KTDotNetCore.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using Refit;

namespace KTDotNetCore.Mvc.Controllers
{
    public class BlogHttpClientController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public BlogHttpClientController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            BlogListResponseModel model = new BlogListResponseModel();
            HttpResponseMessage response = await _httpClient.GetAsync("api/blog");
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<BlogListResponseModel>(jsonStr)!;
            }
            return View("~/Views/BlogRefit/Index.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Save(BlogDataModels blog)
        {
            try
            {
                string blogJson = JsonConvert.SerializeObject(blog);
                HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json);
                var response = await _httpClient.PostAsync($"https://localhost:7206/api/blog", httpContent);

                return Redirect("/BlogHttpClient");
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

        public async Task<IActionResult> Create()
        {
            return View("~/Views/BlogRefit/Create.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            BlogResponseModel model = new BlogResponseModel();
            HttpResponseMessage response = await _httpClient.GetAsync($"https://localhost:7206/api/blog/{id}");
            
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr)!;
                var item = model!.Data;

                return View("~/Views/BlogRefit/Edit.cshtml", item);
            }
            return Redirect("/BlogHttpClient");

        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, BlogDataModels reqModel)
        {
            BlogResponseModel model = new BlogResponseModel();
            string blog = JsonConvert.SerializeObject(reqModel);
            HttpContent httpContent = new StringContent(blog, Encoding.UTF8, Application.Json);
            HttpResponseMessage response = await _httpClient.PutAsync($"https://localhost:7206/api/blog/{id}", httpContent);
            if (!response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr)!;

                TempData["Message"] = "No data found.";
                TempData["IsSuccess"] = false;
            }
            else  if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Updating Successful.";
                TempData["IsSuccess"] = true;
            }
            return Redirect("/BlogHttpClient");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            BlogResponseModel model = new BlogResponseModel();
            var response = await _httpClient.DeleteAsync($"https://localhost:7206/api/Blog/{id}");

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr)!;
                TempData["Message"] = "Deleting Successful.";
                TempData["IsSuccess"] = true;
            }
            else
            {
                TempData["Message"] = "No data found.";
                TempData["IsSuccess"] = false;
            }
            return Redirect("/BlogHttpClient");
        }
    }
}
