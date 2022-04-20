using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Salary
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        [Required(ErrorMessage = "Please Enter Total Salary")]
        public decimal TotalSalary { get; set; }

        public int EmpId { get; set; }
        [ForeignKey("EmpId")]

        public virtual Employee Employee { get; set; }
    }
}
