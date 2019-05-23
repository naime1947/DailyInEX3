using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyInEx.Models
{
    public class CompanyModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }
    }
}