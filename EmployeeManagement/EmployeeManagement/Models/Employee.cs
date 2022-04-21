using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string EmpName { get; set; }
        public string Gender { get; set; }
        
        public int DesignationId { get; set; }
        [ForeignKey("DepartmentId")]

        public virtual Designation Designation { get; set; }

        public int Age { get; set; }
        public DateTime JoinDate { get; set; }
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        public decimal TotalSalary { get; set; }

        public List<Salary> Salary { get; set; }
    }
}
