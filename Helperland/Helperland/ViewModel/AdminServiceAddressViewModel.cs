using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class AdminServiceAddressViewModel
    {
        [Required(ErrorMessage = "Enter Street name")]
        public string c_street { get; set; }

        [RegularExpression(@"^(\d{1,3})$", ErrorMessage = "Invalid House number")]
        [Required(ErrorMessage = "Enter House number")]
        public int c_hno { get; set; }

        [RegularExpression(@"^[1-9]{1}[0-9]{5}$", ErrorMessage = "enter valid zipcode")]
        [Required(ErrorMessage = "Enter Postal code")]
        public string c_postal { get; set; }

        [Required(ErrorMessage = "Enter city name")]
        public string c_city { get; set; }
    }
}
