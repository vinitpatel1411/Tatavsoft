using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class EmployeeSalaryViewModel
    {
        [Required(ErrorMessage = "Please Enter Employee Name")]
        [Display(Name = "Employee Name")]
        public string EmpName { get; set; }
        [Required(ErrorMessage = "Please Enter Designation")]
        [Display(Name = "Designation")]
        public string Designation { get; set; }
        [Required(ErrorMessage = "Please Enter Department")]
        [Display(Name = "Department")]
        public string DepartmentName { get; set; }
        [Required(ErrorMessage = "Please Select Gender")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Please Enter Total Salary")]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Salary")]
        public decimal TotalSalary { get; set; }
        [Required(ErrorMessage = "Please Enter Joining Date")]
        [Display(Name = "Joining Date")]
        public DateTime JoinDate { get; set; }

        [Range(22, 60, ErrorMessage = "Age must be between 22 and 60")]
        [Required(ErrorMessage = "Please Enter Age")]
        [Display(Name = "Age")]
        public int Age { get; set; }
    }
}
