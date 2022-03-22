using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class MySetingDetailViewModel
    {
        [Required(ErrorMessage ="Enter Firstname")]
        public string fname { get; set; }

        [Required(ErrorMessage = "Enter Lastname")]
        public string lname { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email format")]
        [Required(ErrorMessage = "Enter Email")]
        public string email { get; set; }

        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Invalid mobile number")]
        [Required(ErrorMessage = "Enter Phonenumber")]
        public string phone { get; set; }
    }
}
