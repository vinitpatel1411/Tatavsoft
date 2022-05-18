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
            List<Employee> emp = _context.Employees.Include(u=>u.Department).Include(u => u.Designation).ToList();
            return emp;
        }

        public Employee GetById(int id)
        {
            var result = _context.Employees.Include(u=>u.Department).Include(u => u.Designation).FirstOrDefault(n => n.Id == id);
            return result;
        }

        public int UpdateEmployeeDetails(Employee newemp)
        {
            _context.Update(newemp);
            _context.SaveChanges();
            var result = newemp.Id;
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
            var result = employee.Id;
            return result;
        }

        public void addDepartment(Department Department)
        {
                _context.Departments.Add(Department);
                _context.SaveChanges();
        }

        public void addSalary(Salary Salary)
        {
            _context.Salaries.Add(Salary);
            _context.SaveChanges();
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

        public List<Designation> GetDesignationList()
        {
            List<Designation> Des= _context.Designations.ToList();
            return Des;
        }

        public List<Country> GetCountryList()
        {
            List<Country> country = _context.Countries.ToList();
            return country;
        }

        public List<State> GetStateList(int id)
        {
            List<State>  state = _context.States.Where(z => z.CountryId == id).ToList();
            return state;
        }

        public List<City> GetCityList(int id)
        {
            List<City> city = _context.Cities.Where(z => z.StateId == id).ToList();
            return city;
        }

        /*public Salary GetSalary(int id)
        {
            var result = _context.Salaries.FirstOrDefault(n => n.EmpId == id);
            return result;
        }*/

        public void DeleteSalary(Salary Sal)
        {
            _context.Remove(Sal);
            _context.SaveChanges();
        }

        public bool dept_exist(string name)
        {
            var dept = _context.Departments.Where(a => a.Name == name).FirstOrDefault();
            if(dept ==  null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Department GetDepartmentListById(int id)
        {
            var result = _context.Departments.FirstOrDefault(n => n.Id == id);
            return result;
        }

        public void EditDepartment(Department Dept)
        {
            _context.Departments.Update(Dept);
            _context.SaveChanges();
        }

        public void DeleteDepartment(Department Dept)
        {
            _context.Departments.Remove(Dept);
            _context.SaveChanges();
        }

        public string GetStatename(int id)
        {
            var result = _context.States.Where(a => a.Id == id).Select(a=>a.Name).FirstOrDefault();
            return result;
        }

        public bool checkValidDepartment(int DeptId)
        {
            var result = _context.Employees.Where(a => a.DepartmentId == DeptId).FirstOrDefault();
            if (result==null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Designation GetDesignationListById(int id)
        {
            var result = _context.Designations.FirstOrDefault(n => n.Id == id);
            return result;
        }

        public bool des_exist(string name)
        {
            var des = _context.Designations.Where(a => a.Name == name).FirstOrDefault();
            if (des == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void addDesignation(Designation Des)
        {
            _context.Designations.Add(Des);
            _context.SaveChanges();
        }

        public void EditDesignation(Designation Des)
        {
            _context.Designations.Update(Des);
            _context.SaveChanges();
        }

        public void DeleteDesignation(Designation Des)
        {
            _context.Designations.Remove(Des);
            _context.SaveChanges();
        }

        public bool checkValidDesignation(int DesId)
        {
            var result = _context.Employees.Where(a => a.DesignationId == DesId).FirstOrDefault();
            if (result == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Employee> GetSearchEmpDetails(string SearchText, int des, int dept, string gender, int country)
        {
            List<Employee> list=null;
            if (!String.IsNullOrEmpty(SearchText) || des != 0 || dept != 0 || gender != null || country != 0)
            {
                list = _context.Employees.Where(x => x.EmpName.Contains(SearchText)
                && (des == 0 || x.DesignationId == des)
                && (dept == 0 || x.DepartmentId == dept)
                && (gender == null || x.Gender == gender)
                && (country == 0 || x.CountryId == country)).Include(u => u.Designation).Include(u => u.Department).ToList();

            }
            if (String.IsNullOrEmpty(SearchText))
            {
                list = _context.Employees.Where(x => (des == 0 || x.DesignationId == des)
                && (dept == 0 || x.DepartmentId == dept)
                && (gender == null || x.Gender == gender)
                && (country == 0 || x.CountryId == country)).Include(u => u.Designation).Include(u => u.Department).ToList();
            }
            return list;
        }

        public IQueryable<Department> GetDepartmentListByquery()
        {
            var list = (from dept in _context.Departments select dept);
            return list;
        }

        public IQueryable<Designation> GetDesignationListByquery()
        {
            var list = (from des in _context.Designations select des);
            return list;
        }

        public IQueryable<Employee> GetEmpDetailsByquery()
        {
           var list = (from emp in _context.Employees select emp);
            return list;
        }
    }
}
