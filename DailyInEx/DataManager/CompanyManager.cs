using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DailyInEx.DataAccess;
using DailyInEx.Models;

namespace DailyInEx.DataManager
{
    public static class CompanyManager
    {
        public static bool SaveCompany(CompanyModel data)
        {
            string sql = "Insert Into Company(companyname, companyEmail, password, address, countryId)"
                         +"values(@CompanyName,@CompanyEmail,@Password,@Address,@CountryId)";

            int rowAffected = SqlDataAccess.SaveData<CompanyModel>(sql, data);
            if (rowAffected > 0)
            {
                return true;
            }

            return false;
        }
    }
}