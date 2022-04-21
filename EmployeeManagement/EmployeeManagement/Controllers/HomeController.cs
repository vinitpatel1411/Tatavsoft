using EmployeeManagement.Data.Services;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
       

        private readonly IEmployeeServices _service;

        public HomeController(IEmployeeServices service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            List<Department> Dept= _service.GetDepartmentList();
            Dept.Insert(0, new Department { Id = 0, Name = "---Select Department---" });
            List<Designation> Des = _service.GetDesignationList();
            Des.Insert(0, new Designation { Id = 0, Name = "---Select Designation---" });
            ViewBag.Deptlist = Dept;
            ViewBag.Deslist = Des;
            return View();
        }
        [HttpPost]
        public IActionResult Index(EmployeeSalaryViewModel employeesalary)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _service.addEmployeeDetails(employeesalary);
            return RedirectToAction(nameof(Details));

        }

        public IActionResult Details()
        {
            var employees = _service.GetEmpDetails();
            ViewBag.Employees = employees;
            return View();
        }

        public IActionResult Edit(int id)
        {
            var employeedetails = _service.GetById(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employeedetails.Id,
                EmpName = employeedetails.EmpName,
                DesId =employeedetails.DesignationId,
                DeptId = employeedetails.DepartmentId,
                TotalSalary = employeedetails.TotalSalary
            };
            List<Department> Dept = _service.GetDepartmentList();
            Dept.Insert(0, new Department { Id = employeedetails.DepartmentId, Name = employeedetails.Department.Name });
            List<Designation> Des = _service.GetDesignationList();
            Des.Insert(0, new Designation { Id = employeedetails.DesignationId, Name = employeedetails.Designation.Name });
            ViewBag.Deptlist = Dept;
            ViewBag.Deslist = Des;
            if (employeedetails == null)
                return View("Not Found");
            else
                return View(employeeEditViewModel);
        }
        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            _service.UpdateEmployeeDetails(model);
            return RedirectToAction(nameof(Details));

        }
        [HttpDelete]
        public IActionResult Delete(int EmployeeId)
        {
            var employeedetails = _service.GetById(EmployeeId);
            if (employeedetails != null)
            {
                _service.DeleteEmployee(employeedetails);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Details));
            }

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
