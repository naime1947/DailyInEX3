using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DailyInEx.Models;
using DailyInEx.DataAccess;

namespace DailyInEx.DataManager
{
    public static class CountryManager
    {
        public static List<CountryModel> LoadCountry()
        {
            string sql = "select * from Country";
            return SqlDataAccess.LoadData<CountryModel>(sql);
        }
    }
}