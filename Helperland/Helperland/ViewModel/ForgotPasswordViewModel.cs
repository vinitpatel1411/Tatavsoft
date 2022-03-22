using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class ForgotPasswordViewModel
    {
        [RegularExpression(@"^.*(?=.{6,14})(?=.*[a-zA-Z])(?=.*\d)(?=.*[@!#$%&?]).*$", ErrorMessage = "Password must have Upper,lower,number and special character")]
        [Required(ErrorMessage = "Please Enter Password")]
        public string fpassword { get; set; }

        [Required(ErrorMessage = "Please confirm your Password")]
        [Compare("fpassword", ErrorMessage = "Password do not match")]
        public string confirmPassword { get; set; }
    }
}
