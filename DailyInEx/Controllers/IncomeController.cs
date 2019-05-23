using DailyInEx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DailyInEx.DataManager;
using DailyInEx.Models.ViewModel;

namespace DailyInEx.Controllers
{
    [Authorize]
    public class IncomeController : Controller
    {
       
        [HttpGet]
        public ActionResult Entry()
        {
            ViewBag.BankList = BankManager.LoadBank();
            return View();
        }


        [HttpPost]
        public ActionResult Entry(IncomeViewModel income)
        {
            IncomeModel data = new IncomeModel();
            #region Income Model data maping
            data.Amount = income.Amount;
            data.BankId = income.BankId;
            data.Date = income.Date;
            data.Particular = income.Particular;
            data.ChequeNo = income.ChequeNo;
            if (income.isCash == 1)
            {
                data.Cash = true;
            }
            else
            {
                data.Cheque = true;
            }
            #endregion


            bool isSaved = IncomeManager.SaveIncome(data);
            if (isSaved)
            {
                ViewBag.Message = "Income Saved";
            }


            ViewBag.BankList = BankManager.LoadBank();
            return View();
        }





    }
}