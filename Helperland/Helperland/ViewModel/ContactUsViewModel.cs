using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class ContactUsViewModel
    {
        [Required(ErrorMessage ="Please Enter Firstname")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please Enter Lastname")]
        public string LastName { get; set; }
        
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email format")]
        [Required(ErrorMessage ="Please Enter Email")]
        public string Email { get; set; }
        public string Subject { get; set; }
        [Required]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong mobile number")]
        public string Phonenumber { get; set; }
        [Required]
        public string Message { get; set; }

        
    }
}
