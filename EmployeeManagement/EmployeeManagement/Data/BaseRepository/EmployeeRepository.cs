using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Data.BaseRepository
{
    public class EmployeeRepository:IEmpoyeeRepository
    {
        private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Employee> GetEmpDetails()
        {
            List<Employee> emp = _context.Employees.Include(u=>u.Department).ToList();
            return emp;
        }

        public Employee GetById(int id)
        {
            var result = _context.Employees.Include(u=>u.Department).FirstOrDefault(n => n.Id == id);
            return result;
        }

        public int UpdateEmployeeDetails(Employee newemp)
        {
            _context.Update(newemp);
            _context.SaveChanges();
            var result = _context.Employees.Where(a => a.EmpName == newemp.EmpName).Select(a => a.Id).FirstOrDefault();
            return result;
        }

        public void DeleteEmployee(Employee emp)
        {
            _context.Remove(emp);
            _context.SaveChanges();
        }

        public int  addEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            var result  = _context.Employees.Where(a => a.EmpName == employee.EmpName).Select(a => a.Id).FirstOrDefault();
            return result;
        }

        public void addDepartment(Department Department)
        {
            Department Dept = _context.Departments.Where(a => a.Name == Department.Name).FirstOrDefault();
            if(Dept == null)
            {
                _context.Departments.Add(Department);
                _context.SaveChanges();
            }
        }

        public void addSalary(Salary Salary)
        {
            _context.Salaries.Add(Salary);
            _context.SaveChanges();
        }

        public int  GetDepartmentId(string name)
        {
            var result = _context.Departments.Where(a => a.Name == name).Select(a => a.Id).FirstOrDefault();
            if(result == 0)
            {
                result = addDepartmentName(name);
                return result;
            }
            else
            {
                return result;
            }
        }
        int addDepartmentName(string name)
        {
            Department dept = new Department();
            dept.Name = name;
            _context.Departments.Add(dept);
            _context.SaveChanges();
            var result = _context.Departments.Where(a => a.Name == name).Select(a => a.Id).FirstOrDefault();
            return result;
        }
        public int GetEmployeeId(string name)
        {
            var result = _context.Employees.Where(a => a.EmpName == name).Select(a => a.Id).FirstOrDefault();
            return result;
        }

        public decimal GetSalaryDetails(int id)
        {
            var result = _context.Salaries.Where(a => a.EmpId == id).Select(a => a.TotalSalary).FirstOrDefault();
            return result;
        }

        public Salary GetSalarymodel(int id)
        {
           var result = _context.Salaries.Where(a => a.EmpId == id).FirstOrDefault();
            return result;
        }

        public void updateSalary(Salary sal)
        {
            _context.Update(sal);
            _context.SaveChanges();
        }

        public List<Department> GetDepartmentList()
        {
            List<Department> Dept = _context.Departments.ToList();
            return Dept;
        }
    }
}
