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
        public int Id { get; set; }
        [Required, Range(1, int.MaxValue, ErrorMessage = "Please Choose Department")]
        [Display(Name = "Department")]
        
        public int DeptId { get; set; }
        [Required, Range(1, int.MaxValue, ErrorMessage = "Please Choose Designation")]
        [Display(Name = "Designation")]
        public int DesId { get; set; }

        [Required(ErrorMessage = "Please Enter Employee Name")]
        [Display(Name = "Employee Name")]
        public string EmpName { get; set; }
        
        public string Designation { get; set; }
        
        public string DepartmentName { get; set; }
        [Required(ErrorMessage = "Please Select Gender")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Please Enter Total Salary")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(5000, int.MaxValue , ErrorMessage = "Please Enter Valid Salary ")]
        [Display(Name = "Salary")]
        public decimal TotalSalary { get; set; }
       
        [Required(ErrorMessage = "Please Enter Joining Date")]
        [Display(Name = "Joining Date")]
        public DateTime JoinDate { get; set; }

        [Range(22, 60, ErrorMessage = "Age must be between 22 and 60")]
        [Required(ErrorMessage = "Please Enter Age")]
        [Display(Name = "Age")]
        public int Age { get; set; }
        [Required(ErrorMessage ="Please Enter Address")]
        [Display(Name ="Address")]
        public string AddressLine { get; set; }
        [Required, Range(1, int.MaxValue, ErrorMessage = "Please Choose Country")]
        [Display(Name="Country")]
        public int CountryId { get; set; }
        [Required, Range(1, int.MaxValue, ErrorMessage = "Please Choose State")]
        [Display(Name = "State")]
        public int StateId { get; set; }
        [Required, Range(1, int.MaxValue, ErrorMessage = "Please Choose City")]
        [Display(Name = "City")]

        public int CityId { get; set; } 

    }
}
