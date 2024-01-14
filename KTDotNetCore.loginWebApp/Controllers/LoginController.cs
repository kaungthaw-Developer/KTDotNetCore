using KTDotNetCore.loginWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using KTDotNetCore.loginWebApp.EFDbContext;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace KTDotNetCore.loginWebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public LoginController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            var readySign = HttpContext.Session.GetString("LoginData");
            if (readySign != null)
            {
                return Redirect("/");
            }
            return View();
        }

        public async Task<IActionResult> SingIn(SingInModel singIn)
        {
            bool exit = await _appDbContext.UserIfo.AsNoTracking().AnyAsync(x => x.PhoneNumber == singIn.Phone && x.Password == singIn.Password && x.IsActive == true);
            if (exit)
            {
                HttpContext.Session.SetString("LoginData", JsonConvert.SerializeObject(singIn));
                return Redirect("/");

            }
            return View("login");
        }
    }
}
