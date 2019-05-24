using DailyInEx.DataManager;
using DailyInEx.Models;
using DailyInEx.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DailyInEx.Controllers
{
    [Authorize]
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

        public ActionResult Report(int? year, int? month)
        {
            int status = 0;

            #region MonthList ViewBag.Months
            ViewBag.Months = new SelectList(Enumerable.Range(1, 12).Select(x =>
      new SelectListItem()
      {
          Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[x - 1],
          Value = x.ToString()
      }), "Value", "Text");
            #endregion
            #region YearList ViewBag.Years
            List<int> years = new List<int>();
            for (int i = DateTime.Today.Year; i >= 2000; i--)
            {
                years.Add(i);
            }
            ViewBag.Years = years;
            #endregion

            if (year != null && month != null)
            {
                status = 1;
                List<ExpenseMonthlyViewModel> expenses = ExpenseManager.LoadExpenseMonthly((int)year, (int)month);
                if (expenses.Count > 0)
                {
                    ViewBag.ExpenseMonthlyList = expenses;
                    ViewBag.Records = expenses.Count();
                    status = 2;
                }

            }
            ViewBag.Status = status;
            return View();
        }

    }
}