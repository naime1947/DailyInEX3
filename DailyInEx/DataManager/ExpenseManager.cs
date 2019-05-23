using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DailyInEx.Models;
using DailyInEx.DataAccess;

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
    }
}