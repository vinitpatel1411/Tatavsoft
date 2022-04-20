using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Data.BaseRepository
{
    public interface IEmpoyeeRepository
    {
        void addEmployee(Employee employee);
        void addDepartment(Department Department);
        void addSalary(Salary Salary);
        List<Employee> GetEmpDetails();
        Employee GetById(int id);
        int GetDepartmentId(string name);
        int GetEmployeeId(string name);
        void UpdateEmployeeDetails(int id, Employee emp);

        void DeleteEmployee(Employee emp);
    }
}
