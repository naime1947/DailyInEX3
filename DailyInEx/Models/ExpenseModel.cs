using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyInEx.Models
{
    public class ExpenseModel
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public int? BankId { get; set; }
        public string ChequeNo { get; set; }
        public string Particular { get; set; }
        public DateTime Date { get; set; }
        public bool Cash { get; set; }
        public bool Cheque { get; set; }
    }
}