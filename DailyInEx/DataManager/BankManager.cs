using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DailyInEx.DataAccess;
using DailyInEx.Models;

namespace DailyInEx.DataManager
{
    public static class BankManager
    {
        public static List<BankModel> LoadBank()
        {
            string sql = "Select * from bank";
            return SqlDataAccess.LoadData<BankModel>(sql);
        }
    }
}