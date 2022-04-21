using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class EmployeeEditViewModel:EmployeeSalaryViewModel
    {
        public int Id { get; set; }
        public int DesId { get; set; }
        public int DeptId { get; set; }

    }
}
