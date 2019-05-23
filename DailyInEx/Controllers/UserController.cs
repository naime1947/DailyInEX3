using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DailyInEx.DataManager;
using DailyInEx.Models;
using DailyInEx.Models.ViewModel;
using System.Web.Security;

namespace DailyInEx.Controllers
{
    public class UserController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            TempData["UserName"] = TempData["UserName"];
            return View();
        }
        

        //Registraion 
        [HttpGet]
        public ActionResult Registration()
        {
            ViewBag.CountryList = CountryManager.LoadCountry();
            return View();
        }

        [HttpPost]
        public ActionResult Registration(CompanyModel data)
        {
            //Pasword Hashing
            data.Password = Crypto.Hash(data.Password);
            bool isSaved = CompanyManager.SaveCompany(data);
            if (isSaved)
            {
                ViewBag.Message = "Your resgistration successfully completed.";
                return View();
            }

            ViewBag.CountryList = CountryManager.LoadCountry();
            return View();
        }

        //Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            login.Password = Crypto.Hash(login.Password);
            CompanyModel data = LoginManager.ValidateLogin(login);

            if (data != null)
            {
                FormsAuthentication.SetAuthCookie(data.CompanyName, false);
                TempData["UserName"] = data.CompanyName;
                return RedirectToAction("Index", "User");
            }

            ModelState.AddModelError("InvalidLogin", "Invalied  login Informations");
            return View();
        }

        //Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

    }
}