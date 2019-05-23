using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DailyInEx.Models.ViewModel
{
    public class IncomeViewModel
    {
        [Required]
        [DataType(DataType.Currency)]
        [Range(0, 9999999999999999.99)]
        public double Amount { get; set; }

        [Display(Name ="Cheque No")]
        public string ChequeNo { get; set; }

        [Display(Name ="Bank")]
        public int? BankId { get; set; }

        public string Particular { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int isCash { get; set; }

       
    }
}