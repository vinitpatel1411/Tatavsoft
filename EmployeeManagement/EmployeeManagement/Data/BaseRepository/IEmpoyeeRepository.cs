using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Data.BaseRepository
{
    public interface IEmpoyeeRepository
    {
        int addEmployee(Employee employee);
        void addDepartment(Department Department);
        void addSalary(Salary Salary);
        List<Employee> GetEmpDetails();
        Employee GetById(int id);
        int GetDepartmentId(string name);
        int GetEmployeeId(string name);
        int  UpdateEmployeeDetails(Employee emp);

        void DeleteEmployee(Employee emp);

        decimal GetSalaryDetails(int id);
        Salary GetSalarymodel(int id);

        void updateSalary(Salary sal);

        List<Department> GetDepartmentList();
    }
}
