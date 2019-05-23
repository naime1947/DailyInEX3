using DailyInEx.DataManager;
using DailyInEx.Models;
using DailyInEx.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DailyInEx.Controllers
{
    public class ExpenseController : Controller
    {
        public ActionResult Entry()
        {
            ViewBag.BankList = BankManager.LoadBank();
            return View();
        }

        [HttpPost]
        public ActionResult Entry(ExpenseViewModel expense)
        {
            ExpenseModel data = new ExpenseModel();
            #region Income Model data maping
            data.Amount = expense.Amount;
            data.BankId = expense.BankId;
            data.Date = expense.Date;
            data.Particular = expense.Particular;
            data.ChequeNo = expense.ChequeNo;
            if (expense.isCash == 1)
            {
                data.Cash = true;
            }
            else
            {
                data.Cheque = true;
            }
            #endregion


            bool isSaved = ExpenseManager.SaveExpense(data);
            if (isSaved)
            {
                ViewBag.Message = "Expense Saved";
            }


            ViewBag.BankList = BankManager.LoadBank();
            return View();
        }
    }
}