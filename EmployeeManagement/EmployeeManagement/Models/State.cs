using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class State
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int CountryId { get; set; }
        
        public virtual Country Country { get; set; }

        public List<City> City { get;set; }
        public List<Employee> Employee { get; set; }
    }
}
