using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DailyInEx.Models.ViewModel
{
    public class CompanyViewModel
    {
        [Display(Name ="Company Name")]
        [Required]
        public string CompanyName { get; set; }

        [Display(Name = "Company Email")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string CompanyEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage ="Please enter at leash 6 character long password")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password and confirm password do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Address { get; set; }

        [Display(Name = "Country")]
        [Required]
        public int CountryId { get; set; }

    }
}