using EmployeeManagement.Data.BaseRepository;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public void AddDepartment(Department Dept)
        {
            _repository.addDepartment(Dept);
        }

        public void AddDesignation(Designation Des)
        {
            _repository.addDesignation(Des);
        }

        public void AddEmployeeDetails(EmployeeSalaryViewModel employeeSalary)
        {
            Employee emp = new Employee();
            emp.EmpName = employeeSalary.EmpName;
            emp.DesignationId =employeeSalary.DesId;
            emp.Gender = employeeSalary.Gender;
            emp.JoinDate = employeeSalary.JoinDate;
            emp.Age = employeeSalary.Age;
            emp.TotalSalary = employeeSalary.TotalSalary;
            emp.DepartmentId = employeeSalary.DeptId;
            emp.Addressline = employeeSalary.AddressLine;
            emp.CountryId = employeeSalary.CountryId;
            emp.StateId = employeeSalary.StateId;
            emp.CityId = employeeSalary.CityId;
            var empid = _repository.addEmployee(emp);
            /*Department dept = new Department();
            dept.Name = employeeSalary.DepartmentName;
            _service.addDepartment(dept);*/
            Salary sal = new Salary();
            sal.EmpId = empid;
            sal.TotalSalary = employeeSalary.TotalSalary;
            _repository.addSalary(sal);
        }

        public bool checkValidDepartment(int Deptid)
        {
            var result = _repository.checkValidDepartment(Deptid);
            return result;
        }

        public bool checkValidDesignation(int Desid)
        {
            var result = _repository.checkValidDesignation(Desid);
            return result;
        }

        public void DeleteDepartment(int id)
        {
            var Dept = _repository.GetDepartmentListById(id);
            _repository.DeleteDepartment(Dept);
        }

        public void DeleteDesignation(int id)
        {
            var Des = _repository.GetDesignationListById(id);
            _repository.DeleteDesignation(Des);
        }

        public void DeleteEmployee(Employee emp)
        {
            int id = emp.Id;
            Salary sal = _repository.GetSalarymodel(id);
            _repository.DeleteSalary(sal);
            _repository.DeleteEmployee(emp);
        }

        public bool dept_exist(string name)
        {
            bool result = _repository.dept_exist(name);
            return result;
        }

        public bool des_exist(string name)
        {
            bool result = _repository.des_exist(name);
            return result;
        }

        public void EditDepartment(int id, string name)
        {
            Department Dept = new Department();
            Dept.Id = id;
            Dept.Name = name;
            _repository.EditDepartment(Dept);
        }

        public void EditDesignation(int id, string name)
        {
            Designation Des = new Designation();
            Des.Id = id;
            Des.Name = name;
            _repository.EditDesignation(Des);
        }

        public Employee GetById(int id)
        {
            var emp = _repository.GetById(id);
            return emp;
        }

        public IEnumerable<SelectListItem> GetCitydropdownList(int StateId)
        {
            var City = _repository.GetCitydropdownList(StateId);
            return City;
        }

        public List<City> GetCityList(int id)
        {
            List<City> city = _repository.GetCityList(id);
            return city;
        }

        public IEnumerable<SelectListItem> GetCountrydropdownList()
        {
            var Country = _repository.GetCountrydropdownList();
            return Country;
        }

        public List<Country> GetCountryList()
        {
            List<Country> country = _repository.GetCountryList();
            return country;
        }

        public IEnumerable<SelectListItem> GetDepartmentdropdownList()
        {
            IEnumerable<SelectListItem> Dept = _repository.GetDepartmentdropdownList();
            return Dept;
        }

        public List<Department> GetDepartmentList()
        {
            List<Department> Dept = _repository.GetDepartmentList();
            return Dept;
        }

        public Department GetDepartmentListById(int id)
        {
            var Dept = _repository.GetDepartmentListById(id);
            return Dept;
        }

        public IQueryable<Department> GetDepartmentListByquery()
        {
            var list = _repository.GetDepartmentListByquery();
            return list;
        }

        public IEnumerable<SelectListItem> GetDesignationdropdownList()
        {
            var Des = _repository.GetDesignationdropdownList();
            return Des;
        }

        public List<Designation> GetDesignationList()
        {
            List<Designation> Des = _repository.GetDesignationList();
            return Des;
        }

        public Designation GetDesignationListById(int id)
        {
            var Des = _repository.GetDesignationListById(id);
            return Des;
        }

        public IQueryable<Designation> GetDesignationListByquery()
        {
            var list = _repository.GetDesignationListByquery();
            return list;
        }

        public List<Employee> GetEmpDetails()
        {
            var Emp = _repository.GetEmpDetails();
            return Emp;
        }

        public IQueryable<Employee> GetEmpDetailsByquery()
        {
            var list = _repository.GetEmpDetailsByquery();
            return list;
        }

        public List<Employee> GetSearchEmpDetails(string SearchText, int des, int dept, string gender, int country)
        {
            var list = _repository.GetSearchEmpDetails(SearchText, des, dept, gender, country);
            return list;
        }

        public IEnumerable<SelectListItem> GetStatedropdownList(int CountryId)
        {
            var State = _repository.GetStatedropdownList(CountryId);
            return State;
        }

        public List<State> GetStateList(int id)
        {
            List<State>  state = _repository.GetStateList(id);
            return state;
        }

        public string GetStatename(int id)
        {
            var name = _repository.GetStatename(id);
            return name;
        }

        public void UpdateEmployeeDetails(EmployeeSalaryViewModel model)
        {
            Employee employee = _repository.GetById(model.Id);
            employee.EmpName = model.EmpName;
            employee.DesignationId = model.DesId;
            employee.TotalSalary = model.TotalSalary;
            employee.DepartmentId = model.DeptId;
            employee.Age = model.Age;
            employee.JoinDate = model.JoinDate;
            employee.Gender = model.Gender;
            employee.Addressline = model.AddressLine;
            employee.CountryId = model.CountryId;
            employee.StateId = model.StateId;
            employee.CityId = model.CityId;
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
