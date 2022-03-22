using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Helperland.Models;

namespace Helperland.ViewModel
{
    public class LoginViewModel
    {

        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email format")]
        [Required(ErrorMessage = "Please Enter Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }
        public bool remember { get; set; }
    }
}
