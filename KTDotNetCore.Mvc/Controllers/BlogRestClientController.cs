using KTDotNetCore.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Reflection.Metadata;

namespace KTDotNetCore.Mvc.Controllers
{
    public class BlogRestClientController : Controller
    {
        private readonly RestClient _restClient;

        public BlogRestClientController(RestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<IActionResult> Index()
        {
            BlogListResponseModel model = new BlogListResponseModel();
            RestRequest request = new RestRequest("api/Blog", Method.Get);

            //await _restClient.GetAsync(request);
            var response = await _restClient.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content;
                model = JsonConvert.DeserializeObject<BlogListResponseModel>(jsonStr)!;
            }
            return View("~/Views/BlogRefit/Index.cshtml", model);

        }

        public IActionResult Create()
        {
            return View("~/Views/BlogRefit/Create.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Save(BlogResponseModel blog)
        {
            RestRequest request = new RestRequest("api/blog/", Method.Post);
            request.AddBody(blog);
            RestResponse response = await _restClient.ExecuteAsync(request);
            TempData["Message"] = "Saving Successful.";
            TempData["IsSuccess"] = true;
            return Redirect("/blogrestclient");
        }

        public async Task<IActionResult> Edit(int id)
        {
            BlogResponseModel model = new BlogResponseModel();
            RestRequest request = new RestRequest($"api/blog/{id}", Method.Get);
            RestResponse response = await _restClient.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content;
                model = JsonConvert.DeserializeObject<BlogResponseModel>(jsonStr)!;
                var item = model!.Data;

                return View("~/Views/BlogRefit/Edit.cshtml", item);
            }
            return Redirect("/BlogRestClient");
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, BlogResponseModel blog)
        {
            RestRequest request = new RestRequest($"api/blog/{id}", Method.Put);
            request.AddBody(blog);
            RestResponse response = await _restClient.ExecuteAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                TempData["Message"] = "No data found.";
                TempData["IsSuccess"] = false;
            }
            else if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Updating Successful.";
                TempData["IsSuccess"] = true;
            }
            return Redirect("/BlogRestClient");
        }

        public async Task<IActionResult> Delete(int id)
        {
            RestRequest request = new RestRequest($"api/blog/{id}", Method.Delete);
            RestResponse response = await _restClient.ExecuteAsync(request);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Deleting Successful.";
                TempData["IsSuccess"] = true;
            }
            else
            {
                TempData["Message"] = "No data found.";
                TempData["IsSuccess"] = false;
            }
            return Redirect("/BlogRestClient");
        }
    }
}
