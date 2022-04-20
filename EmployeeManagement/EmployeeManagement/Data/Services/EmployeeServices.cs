using EmployeeManagement.Data.BaseRepository;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Data.Services
{
    public class EmployeeServices:IEmployeeServices
    {
        private readonly IEmpoyeeRepository _service;

        public EmployeeServices(IEmpoyeeRepository service)
        {
            _service = service;
        }

        public void addEmployeeDetails(EmployeeSalaryViewModel employeeSalary)
        {
            Employee emp = new Employee();
            emp.EmpName = employeeSalary.EmpName;
            emp.Designation = employeeSalary.Designation;
            emp.Gender = employeeSalary.Gender;
            emp.JoinDate = employeeSalary.JoinDate;
            emp.Age = employeeSalary.Age;
            emp.TotalSalary = employeeSalary.TotalSalary;
            emp.DepartmentId = _service.GetDepartmentId(employeeSalary.DepartmentName);
            _service.addEmployee(emp);
            Department dept = new Department();
            dept.Name = employeeSalary.DepartmentName;
            _service.addDepartment(dept);
            Salary sal = new Salary();
            sal.EmpId = _service.GetEmployeeId(employeeSalary.EmpName);
            sal.TotalSalary = employeeSalary.TotalSalary;
            _service.addSalary(sal);
        }

        public void DeleteEmployee(Employee emp)
        {
            _service.DeleteEmployee(emp);
        }

        public Employee GetById(int id)
        {
            var emp = _service.GetById(id);
            return emp;
        }

        public List<Employee> GetEmpDetails()
        {
            var Emp = _service.GetEmpDetails();
            return Emp;
        }

        public void UpdateEmployeeDetails(EmployeeEditViewModel model)
        {
            Employee employee = _service.GetById(model.Id);
            employee.EmpName = model.EmpName;
            employee.Designation = model.Designation;
            employee.TotalSalary = model.TotalSalary;
            employee.DepartmentId = _service.GetDepartmentId(model.DepartmentName);
            _service.UpdateEmployeeDetails(employee);
        }
    }
}
