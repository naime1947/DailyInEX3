using DailyInEx.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace DailyInEx.DataManager
{
    public static class YearlyReportManager
    {
        public static List<MonthlyInExProfit> LoadMonthlyInExProfitList(int year)
        {
            List<MonthlyInExProfit> monthlyIncomeList = new List<MonthlyInExProfit>();
            
            List<IncomeMonthlyViewModel> inocmeList = new List<IncomeMonthlyViewModel>();
            List<ExpenseMonthlyViewModel> expenseList = new List<ExpenseMonthlyViewModel>();

            for (int month = 1; month<=12; month++)
            {
                MonthlyInExProfit aMonthlyInExProfit = new MonthlyInExProfit();

                inocmeList = IncomeManager.LoadIncomeMonthly(year, month);
                foreach (var data in inocmeList)
                {
                    aMonthlyInExProfit.MonthlyTotlaIncome += data.Amount;
                }

                expenseList = ExpenseManager.LoadExpenseMonthly(year, month);
                foreach (var data in expenseList)
                {
                    aMonthlyInExProfit.MonthlyTotalExpense += data.Amount;
                }

                aMonthlyInExProfit.Month = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(month);
                monthlyIncomeList.Add(aMonthlyInExProfit);
            }

            return monthlyIncomeList;
        }

        
    }
}