using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyInEx.Models.ViewModel
{
    public class ExpenseMonthlyViewModel
    {
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public bool Cash { get; set; }
        public bool Cheque { get; set; }
        public string ChequeNo { get; set; }
        public string BankName { get; set; }
        public string Particular { get; set; }
    }
}