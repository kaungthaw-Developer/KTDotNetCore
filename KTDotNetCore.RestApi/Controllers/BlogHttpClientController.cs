using Microsoft.AspNetCore.Mvc;

namespace KTDotNetCore.RestApi.Controllers
{
    public class BlogHttpClientController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
