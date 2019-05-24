using DailyInEx.DataManager;
using DailyInEx.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DailyInEx.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        public ActionResult Yearly(int? year)
        {
            #region YearList ViewBag.Years
            List<int> years = new List<int>();
            for (int i = DateTime.Today.Year; i >= 2000; i--)
            {
                years.Add(i);
            }
            ViewBag.Years = years;
            #endregion

            if (year != null)
            {
                List<MonthlyInExProfit> monthlyInExProfitList = YearlyReportManager.LoadMonthlyInExProfitList((int) year);
                ViewBag.DataList = monthlyInExProfitList;
            }
            
            return View();
        }
    }
}