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
        public static List<IncomeModel> LoadUnApprovedIncome()
        {
            string sql = "select * from income where IsApproved is null";
            return SqlDataAccess.LoadData<IncomeModel>(sql);
        }

        public static bool UpdateApprovedIncome(List<int> approvedIds)
        {
            string sql = "";
            foreach (int id in approvedIds)
            {
                sql = "Update Income set IsApproved = 1 where Id =" + id;
                int rowAffected =  SqlDataAccess.UpdateData(sql);
                if (rowAffected == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}