using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DailyInEx.Models;
using DailyInEx.DataAccess;
using DailyInEx.Models.ViewModel;

namespace DailyInEx.DataManager
{
    public static class ExpenseManager
    {
        public static bool SaveExpense(ExpenseModel data)
        {
            string sql = "Insert Into Expense(Amount, Cash, Cheque, ChequeNo, BankId, Particular, Date)"
                         + "Values(@Amount, @Cash, @Cheque, @ChequeNo, @BankId, @Particular, @Date)";

            int rowAffected = SqlDataAccess.SaveData<ExpenseModel>(sql, data);
            if (rowAffected > 0)
            {
                return true;
            }
            return false;
        }
        public static List<ExpenseModel> LoadUnApprovedIncome()
        {
            string sql = "select * from expense where IsApproved is null";
            return SqlDataAccess.LoadData<ExpenseModel>(sql);
        }

        public static bool UpdateApprovedExpense(List<int> approvedIds)
        {
            string sql = "";
            foreach (int id in approvedIds)
            {
                sql = "Update Expense set IsApproved = 1 where Id =" + id;
                int rowAffected = SqlDataAccess.UpdateData(sql);
                if (rowAffected == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static List<ExpenseMonthlyViewModel> LoadExpenseMonthly(int year, int month)
        {
            string sql = "select * from ExpenseMonthlyWithBank";
            List<ExpenseMonthlyViewModel> expenses = SqlDataAccess.LoadData<ExpenseMonthlyViewModel>(sql);
            List<ExpenseMonthlyViewModel> expenseInYearMonth = new List<ExpenseMonthlyViewModel>();
            foreach (ExpenseMonthlyViewModel data in expenses)
            {
                if (data.Date.Month == month && data.Date.Year == year)
                {
                    expenseInYearMonth.Add(data);
                }
            }

            return expenseInYearMonth;

        }

    }
}