using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace DailyInEx.DataAccess
{
    public static class SqlDataAccess
    {
        public static string GetConnectionString(string connectionName = "DailyInExDB")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }


        //for  Saving Data into database
        public static int SaveData<T>(string sql, T data)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Execute(sql, data);
            }
        }


        //for loading data
        public static List<T> LoadData<T>(string sql)
        {
            using (IDbConnection cnn = new SqlConnection(GetConnectionString()))
            {
                return cnn.Query<T>(sql).ToList();
            }
        }
    }
}