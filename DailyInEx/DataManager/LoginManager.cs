using DailyInEx.DataAccess;
using DailyInEx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DailyInEx.Models.ViewModel;

namespace DailyInEx.DataManager
{
    public static class LoginManager
    {
        public static CompanyModel ValidateLogin(LoginViewModel login)
        {
            string sql = "select * from Company";
            List<CompanyModel> companies = SqlDataAccess.LoadData<CompanyModel>(sql);

            foreach (CompanyModel data in companies)
            {
                if(data.CompanyEmail==login.Email && data.Password == login.Password)
                {
                    return data;
                }
            }

            return null;
        }
    }
}