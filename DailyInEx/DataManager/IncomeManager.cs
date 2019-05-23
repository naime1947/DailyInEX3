using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DailyInEx.Models;
using DailyInEx.DataAccess;

namespace DailyInEx.DataManager
{
    public static class IncomeManager
    {
        public static bool SaveIncome(IncomeModel data)
        {
            string sql = "Insert Into Income(Amount, Cash, Cheque, ChequeNo, BankId, Particular, Date)"
                         + "Values(@Amount, @Cash, @Cheque, @ChequeNo, @BankId, @Particular, @Date)";

            int rowAffected = SqlDataAccess.SaveData<IncomeModel>(sql, data);
            if (rowAffected > 0)
            {
                return true;
            }
            return false;
        }
    }
}