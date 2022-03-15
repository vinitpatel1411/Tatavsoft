using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class CustomerRegistrationViewModel
    {
        [Required(ErrorMessage = "Please Enter Firstname")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Firstname")]
        public string LastName { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email format")]
        [Required(ErrorMessage = "Please Enter Email")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your Password")]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string confirmPassword { get; set; }
        [Required]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong mobile number")]
        public string Phonenumber { get; set; }
        

    }
}
