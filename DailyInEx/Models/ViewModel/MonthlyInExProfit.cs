using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyInEx.Models.ViewModel
{
    public class MonthlyInExProfit
    {
        public string Month { get; set; }
        public double MonthlyTotlaIncome { get; set; }
        public double MonthlyTotalExpense { get; set; }
        public double MonthlyProfit
        {
            get
            {
                return MonthlyTotlaIncome - MonthlyTotalExpense;
            }
        }
    }
}