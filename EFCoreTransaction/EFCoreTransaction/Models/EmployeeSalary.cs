using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreTransaction.Models
{
    public class EmployeeSalary
    {
        public Employee Employee { get; set; }
        public Salary Salary { get; set; }
    }
}
