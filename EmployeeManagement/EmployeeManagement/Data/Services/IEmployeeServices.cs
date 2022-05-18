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
        void AddEmployeeDetails(EmployeeSalaryViewModel employeeSalary);
        List<Employee> GetEmpDetails();
        
        void UpdateEmployeeDetails(EmployeeSalaryViewModel model);

        void DeleteEmployee(Employee emp);

        Employee GetById(int id);

        Department GetDepartmentListById(int id);

        Designation GetDesignationListById(int id);
        List<Department> GetDepartmentList();

        List<Designation> GetDesignationList();
        List<Country> GetCountryList();
        List<State> GetStateList(int id);
        List<City> GetCityList(int id);

        void AddDepartment(Department Dept);
        void AddDesignation(Designation Des);
        bool dept_exist(string name);
        bool des_exist(string name);

        void EditDepartment(int id, string name);
        void EditDesignation(int id, string name);
        void DeleteDepartment(int id);
        void DeleteDesignation(int id);
        String GetStatename(int id);
        
        bool checkValidDepartment(int Deptid);

        bool checkValidDesignation(int Desid);

        List<Employee> GetSearchEmpDetails(string SearchText,int  des,int dept,string gender,int country);

        IQueryable<Department> GetDepartmentListByquery();

        IQueryable<Designation> GetDesignationListByquery();

        IQueryable<Employee> GetEmpDetailsByquery();
    }
}
