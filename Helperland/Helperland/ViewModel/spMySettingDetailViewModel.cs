using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class spMySettingDetailViewModel
    {
        [Required(ErrorMessage = "Enter Firstname")]
        public string Firsname { get; set; }

        [Required(ErrorMessage = "Enter Lastname")]
        public string Lastname { get; set; }


        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Invalid mobile number")]
        [Required(ErrorMessage = "Enter Phonenumber")]
        public string phone { get; set; }

        [Required(ErrorMessage = "Enter Street name")]
        public string street { get; set; }

        [RegularExpression(@"^(\d{1,3})$", ErrorMessage = "Invalid House number")]
        [Required(ErrorMessage = "Enter House number")]
        public int house { get; set; }

        [RegularExpression(@"^[1-9]{1}[0-9]{5}$", ErrorMessage = "enter valid zipcode")]
        [Required(ErrorMessage = "Enter Postal code")]
        public string postal { get; set; }

        [Required(ErrorMessage = "Enter city name")]
        public string city { get; set; }
    }
}
