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
        private readonly IEmpoyeeRepository _repository;

        public EmployeeServices(IEmpoyeeRepository repository)
        {
            _repository = repository;
        }

        public void addEmployeeDetails(EmployeeSalaryViewModel employeeSalary)
        {
            Employee emp = new Employee();
            emp.EmpName = employeeSalary.EmpName;
            emp.DesignationId = Int32.Parse(employeeSalary.Designation);
            emp.Gender = employeeSalary.Gender;
            emp.JoinDate = employeeSalary.JoinDate;
            emp.Age = employeeSalary.Age;
            emp.TotalSalary = employeeSalary.TotalSalary;
            emp.DepartmentId = Int32.Parse(employeeSalary.DepartmentName);
            var empid = _repository.addEmployee(emp);
            /*Department dept = new Department();
            dept.Name = employeeSalary.DepartmentName;
            _service.addDepartment(dept);*/
            Salary sal = new Salary();
            sal.EmpId = empid;
            sal.TotalSalary = employeeSalary.TotalSalary;
            _repository.addSalary(sal);
        }

        public void DeleteEmployee(Employee emp)
        {
            _repository.DeleteEmployee(emp);
        }

        public Employee GetById(int id)
        {
            var emp = _repository.GetById(id);
            return emp;
        }

        public List<Department> GetDepartmentList()
        {
            List<Department> Dept = _repository.GetDepartmentList();
            return Dept;
        }

        public List<Designation> GetDesignationList()
        {
            List<Designation> Des = _repository.GetDesignationList();
            return Des;
        }

        public List<Employee> GetEmpDetails()
        {
            var Emp = _repository.GetEmpDetails();
            return Emp;
        }

        public void UpdateEmployeeDetails(EmployeeEditViewModel model)
        {
            Employee employee = _repository.GetById(model.Id);
            employee.EmpName = model.EmpName;
            employee.DesignationId = model.DesId;
            employee.TotalSalary = model.TotalSalary;
            employee.DepartmentId = model.DeptId;
            var result =_repository.UpdateEmployeeDetails(employee);
            var salary = _repository.GetSalaryDetails(model.Id);
            if (!(salary == model.TotalSalary))
            {
                Salary sal = _repository.GetSalarymodel(model.Id);
                sal.TotalSalary = model.TotalSalary;
                _repository.updateSalary(sal);

            }
        }
    }
}
