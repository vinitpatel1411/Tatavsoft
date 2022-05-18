using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Designation
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Designation Name")]
        [Required(ErrorMessage = "Please Enter Designation Name")]
        public string Name { get; set; }

        public List<Employee> Employee { get; set; }
    }
}
