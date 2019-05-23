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

        [HttpGet]
        public ActionResult Approve()
        {
            string message = "";
            bool status = false;
            List<ExpenseModel> expenses = ExpenseManager.LoadUnApprovedIncome();
            if (expenses.Count()>0)
            {
                ViewBag.UnApprovedExpenses = expenses;
                message = expenses.Count() + "unapproved expense found";
                status = true;
            }
            else
            {
                message = "Nothing to approve now";
                status = false;
            }
            ViewBag.Message = message;
            ViewBag.Status = status;
            return View();
        }

        [HttpPost]
        public ActionResult Approve(FormCollection Id)
        {

            #region Getting Selected Id
            List<int> approvedIds = new List<int>();
            var arr = Id["Id"].ToString().Split(',');
            foreach (string id in arr)
            {
                approvedIds.Add(Convert.ToInt32(id));
            }
            #endregion

            bool isUpdated = ExpenseManager.UpdateApprovedExpense(approvedIds);

            return RedirectToAction("Approve", "Expense");
        }

    }
}