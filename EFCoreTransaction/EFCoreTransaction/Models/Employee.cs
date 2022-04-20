using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreTransaction.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpId { get; set; }
        [Required(ErrorMessage = "Employee Name")]
        public string EmpName { get; set; }
        [Required]
        public string Designation { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public string JoinDate { get; set; }
    }
}
