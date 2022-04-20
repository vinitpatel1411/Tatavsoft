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
            ViewBag.Deptlist = new List<string>() { "Development", "Testing", "QA", "Management", "Sales", "Finance" };
            return View();
        }
        [HttpPost]
        public IActionResult Index(EmployeeSalaryViewModel employeesalary)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            ViewBag.Deptlist = new List<string>() { "Development", "Testing", "QA", "Management", "Sales", "Finance" };
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
            ViewBag.Deptlist = new List<string>() { "Development", "Testing", "QA", "Management", "Sales", "Finance" };
            var employeedetails = _service.GetById(id);
            if (employeedetails == null)
                return View("Not Found");
            else
                return View(employeedetails);
        }
        [HttpPost]
        public IActionResult Edit(int id, [Bind("EmpId,EmpName,Designation,Department,TotalSalary")] Employee emp)
        {
            if (!ModelState.IsValid)
            {
                return View(emp);
            }
            _service.UpdateEmployeeDetails(id, emp);
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
