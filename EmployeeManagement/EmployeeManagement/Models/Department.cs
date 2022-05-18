using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Display(Name="Departmnet Name")]
        [Required(ErrorMessage ="Please Enter Department Name")]
        public string Name { get; set; }

        public List<Employee> Employee { get; set; }
    }
}
