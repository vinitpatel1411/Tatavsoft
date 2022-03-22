using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class MySettingAddressViewModel
    {
        [Required(ErrorMessage = "Enter Street name")]
        public string street { get; set; }

        [RegularExpression(@"^(\d{1,3})$", ErrorMessage = "Invalid House number")]
        [Required(ErrorMessage = "Enter House number")]
        public int hno { get; set; }

        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Invalid mobile number")]
        [Required(ErrorMessage = "Enter Phonenumber")]
        public string phone { get; set; }

        [RegularExpression(@"^[1-9]{1}[0-9]{5}$", ErrorMessage = "enter valid zipcode")]
        [Required(ErrorMessage = "Enter Postal code")]
        public string pincode { get; set; }

        [Required(ErrorMessage = "Enter city name")]
        public string city { get; set; }
    }
}
