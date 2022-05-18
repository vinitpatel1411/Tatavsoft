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

        void addDesignation(Designation Des);
        void addSalary(Salary Salary);
        List<Employee> GetEmpDetails();
        Employee GetById(int id);
        Department GetDepartmentListById(int id);
        Designation GetDesignationListById(int id);
        int  UpdateEmployeeDetails(Employee emp);

        void DeleteEmployee(Employee emp);
        void DeleteSalary(Salary Sal);
        decimal GetSalaryDetails(int id);
        Salary GetSalarymodel(int id);

        void updateSalary(Salary sal);

        List<Department> GetDepartmentList();

        List<Designation> GetDesignationList();
        List<Country> GetCountryList();
        List<State> GetStateList(int id);
        List<City> GetCityList(int id);

        bool dept_exist(string name);
        bool des_exist(string name);

        void EditDepartment(Department Dept);
        void EditDesignation(Designation Des);
        void DeleteDepartment(Department Dept);
        void DeleteDesignation(Designation Des);
        String GetStatename(int id);

        bool checkValidDepartment(int DeptId);

        bool checkValidDesignation(int DesId);

        List<Employee> GetSearchEmpDetails(string SearchText,int des,int dept,string gender,int country);

        IQueryable<Department> GetDepartmentListByquery();

        IQueryable<Designation> GetDesignationListByquery();

        IQueryable<Employee> GetEmpDetailsByquery();
    }
}
