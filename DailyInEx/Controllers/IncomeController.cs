using DailyInEx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DailyInEx.DataManager;
using DailyInEx.Models.ViewModel;
using System.Globalization;

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

        [HttpGet]
        public ActionResult Approve()
        {
            string message = "";
            bool status = false;
            List<IncomeModel> incomes = IncomeManager.LoadUnApprovedIncome();
            if (incomes.Count() > 0)
            {
                ViewBag.UnApprovedIncome = incomes;
                message = incomes.Count() + "Un approved incomes found";
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

            bool isUpdated = IncomeManager.UpdateApprovedIncome(approvedIds);

            return RedirectToAction("Approve", "Income");
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

            if(year!=null && month!= null){
                status = 1;
                List<IncomeMonthlyViewModel> incomes = IncomeManager.LoadIncomeMonthly((int)year, (int)month);
                if (incomes.Count > 0)
                {
                    ViewBag.IncomeMonthlyList = incomes;
                    ViewBag.Records = incomes.Count();
                    status = 2;
                }
                
            }
            ViewBag.Status = status;
            return View();
        }

    }
}