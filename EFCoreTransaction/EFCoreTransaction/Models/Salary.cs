using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreTransaction.Models
{
    public class Salary
    {
        [Key]
        public int SalaryId { get; set; }
        public int EmpId { get; set; }
        [Column(TypeName="decimal(10,2)")]
        [Required(ErrorMessage = "Please Enter Total Salary")]
        public decimal TotalSalary { get; set; }
    }
}
