using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Data.Services
{
    public interface IEmployeeServices
    {
        void addEmployeeDetails(EmployeeSalaryViewModel employeeSalary);
        List<Employee> GetEmpDetails();
        
        void UpdateEmployeeDetails(EmployeeEditViewModel model);

        void DeleteEmployee(Employee emp);

        Employee GetById(int id);

        List<Department> GetDepartmentList();

        List<Designation> GetDesignationList();
    }
}
