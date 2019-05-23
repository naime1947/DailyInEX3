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

        [HttpGet]
        public ActionResult Approve()
        {
            string message = "";
            bool status = false;
            List<IncomeModel> incomes = IncomeManager.LoadUnApprovedIncome();
            if (incomes.Count() > 0)
            {
                ViewBag.UnApprovedIncome = incomes;
                message = incomes.Count()+"Un approved incomes found";
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



    }
}