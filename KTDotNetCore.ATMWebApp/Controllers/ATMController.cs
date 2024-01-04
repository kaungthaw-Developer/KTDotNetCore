using KTDotNetCore.ATMWebApp.EFDbContext;
using KTDotNetCore.ATMWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using IdGen;
using System;

namespace KTDotNetCore.ATMWebApp.Controllers
{

    public class ATMController : Controller
    {
        private readonly ATMDbContext _atmDbContext;

        private readonly IdGenerator _idGenerator = new IdGenerator(0);

        public ATMController(ATMDbContext atmDbContext)
        {
            _atmDbContext = atmDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SingIn(SingInModel singIn)
        {
            bool exit = await _atmDbContext.UserIfo.AsNoTracking().AnyAsync(x => x.PhoneNumber == singIn.Phone && x.Password == singIn.Password && x.IsActive == true);
            if (exit )
            {
                var userDetail = await _atmDbContext.UserIfo.AsNoTracking().FirstOrDefaultAsync(x => x.PhoneNumber == singIn.Phone && x.Password == singIn.Password && x.IsActive == true);
                HttpContext.Session.SetString("LoginData", JsonConvert.SerializeObject(singIn));
                var readySign = HttpContext.Session.GetString("LoginData");
                return View("UserHome", userDetail);

            }
            return View("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UserHome()
        {
            var readySign = HttpContext.Session.GetString("LoginData");
            if (readySign is not null)
            {
            SingInModel signIn = JsonConvert.DeserializeObject<SingInModel>(readySign);
                var userDetail = await _atmDbContext.UserIfo.AsNoTracking().FirstOrDefaultAsync(x => x.PhoneNumber == signIn.Phone && x.Password == signIn.Password && x.IsActive == true);
                return View(userDetail);
            }
            return Redirect("Index");
        }

        [HttpGet]
        public IActionResult UserCreate()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> UserCreate(UserDataModel userData)
        {
            var number = await _atmDbContext.UserIfo.AsNoTracking().FirstOrDefaultAsync(x => x.PhoneNumber == userData.PhoneNumber);
            if (number != null)
            {
                TempData["Message"] = "Phone existing in Our DataBase";
                return RedirectToAction("UserCreate");
            }
            userData.UserID = _idGenerator.CreateId().ToString();
            userData.IsActive = true;
            userData.TotalAmount = 0.00;
            userData.CardNumber = _idGenerator.CreateId().ToString();
            await _atmDbContext.UserIfo.AddAsync(userData);
            var result = await _atmDbContext.SaveChangesAsync();

            TempData["AlertMessage"] ="Please Copy ur Card Number - "+ userData.CardNumber;
            return RedirectToAction("index");

        }

        [HttpPost]
        public async Task<IActionResult> Histroy(string userID)
        {

            bool logIn = await _atmDbContext.History.AsNoTracking().AnyAsync(x => x.UserID == userID);
            if (logIn)
            {
                List<UserHistoryModel> userHistories = await _atmDbContext.History.AsNoTracking().Where(x => x.UserID == userID).OrderByDescending(x => x.LastUpdate).ToListAsync();
                var amount = await _atmDbContext.UserIfo.AsNoTracking().Where(x => x.UserID == userID).Select(x => new { x.TotalAmount }).FirstOrDefaultAsync();
                var history = new
                {
                    UserHistories = userHistories,
                    Amount = amount
                };
                return Json(history);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Nextpage(string userID)
        {
            bool logIn = await _atmDbContext.UserIfo.AsNoTracking().AnyAsync(x => x.UserID == userID && x.IsActive == true);
            if (logIn)
            {
                var userInform = await _atmDbContext.UserIfo.AsNoTracking().FirstOrDefaultAsync(x => x.UserID == userID && x.IsActive == true);

                var viewModel = new UserDataModel
                {
                    UserID = userInform.UserID,
                };
                return Json("cashOut");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CashOut()
        {

            return View();
        }
        
        [HttpPost]
        [ActionName("reqeust")]
        public async Task<IActionResult> CashOut(CashOutModel cashOut)
        {
           var withDraw = await _atmDbContext.UserIfo.FirstOrDefaultAsync(x => x.UserID == cashOut.UserID && x.CardNumber == cashOut.CardNumber && x.PinCode == cashOut.PinCode);
                if (withDraw != null)
                {
                    withDraw.TotalAmount -= cashOut.MoneyAmount;
                    UserHistoryModel history = new UserHistoryModel()
                    {
                        HistoryID = _idGenerator.CreateId().ToString(),
                        UserID = cashOut.UserID,
                        CashOutAmount = cashOut.MoneyAmount,
                        LastUpdate = DateTime.Now,
                        Status = "with Draw"
                    };

                    await _atmDbContext.History.AddAsync(history);
             var result = await _atmDbContext.SaveChangesAsync();

                }
                    return View();   
        }

        [HttpGet]
        public IActionResult Deposit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Deposit(CashOutModel cashIn)
        {   
                var withDraw = await _atmDbContext.UserIfo.FirstOrDefaultAsync(x => x.UserID == cashIn.UserID && x.CardNumber == cashIn.CardNumber && x.PinCode == cashIn.PinCode);
                if (withDraw != null)
                {
                    withDraw.TotalAmount += cashIn.MoneyAmount;
                    UserHistoryModel history = new UserHistoryModel()
                    {
                        HistoryID = _idGenerator.CreateId().ToString(),
                        UserID = cashIn.UserID,
                        CashOutAmount = cashIn.MoneyAmount,
                        LastUpdate = DateTime.Now,
                        Status = "Deposit"
                    };

                    await _atmDbContext.History.AddAsync(history);
                var result = await _atmDbContext.SaveChangesAsync();
                return View();
            }
            return View();
        }
    }
}
