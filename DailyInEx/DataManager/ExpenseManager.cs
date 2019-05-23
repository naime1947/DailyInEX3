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
    }
}