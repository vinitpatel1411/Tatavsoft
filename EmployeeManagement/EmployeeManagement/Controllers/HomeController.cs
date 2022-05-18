using EmployeeManagement.Data;
using EmployeeManagement.Data.Services;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
//using System.Web.Mvc;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {

        private readonly IEmployeeServices _service;

        public HomeController(IEmployeeServices service)
        {
            _service = service;
        }
        [HttpPost]
        public JsonResult GetEmployees()
        {

            
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                // Skiping number of Rows count  
                var start = Request.Form["start"].FirstOrDefault();
                // Paging Length 10,20  
                var length = Request.Form["length"].FirstOrDefault();
                // Sort Column Name  
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                // Sort Column Direction ( asc ,desc)  
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10,20,50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all Customer data  
                var customerData = _service.GetEmpDetails();
                var customerData1 = _service.GetEmpDetailsByquery();
                //var customerData1 = (from emp in _context.Employees select emp);

                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    customerData = customerData1.OrderBy(sortColumn + " " + sortColumnDirection).ToList();
                }

                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(m => m.EmpName.Contains(searchValue)).ToList();
                }

                //total number of rows count   
                recordsTotal = customerData.Count();
                //Paging   
                var data = customerData.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data  

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult CreateEdit(int id)
        {
            if(id<=0)
            {
                List<Country> country = _service.GetCountryList();
                country.Insert(0, new Country { Id = 0, Name = "---Select Country---" });
                List<Department> Dept = _service.GetDepartmentList();
                Dept.Insert(0, new Department { Id = 0, Name = "---Select Department---" });
                List<Designation> Des = _service.GetDesignationList();
                Des.Insert(0, new Designation { Id = 0, Name = "---Select Designation---" });
                ViewBag.Deptlist = Dept;
                ViewBag.Deslist = Des;
                ViewBag.countrylist = country;
                return View("CreateEdit", new EmployeeSalaryViewModel());
            }
            else
            {
                var employeedetails = _service.GetById(id);
                EmployeeSalaryViewModel employeesalaryViewModel = new EmployeeSalaryViewModel
                {
                    Id = employeedetails.Id,
                    EmpName = employeedetails.EmpName,
                    DesId = employeedetails.DesignationId,
                    DeptId = employeedetails.DepartmentId,
                    TotalSalary = employeedetails.TotalSalary,
                    Age = employeedetails.Age,
                    JoinDate = employeedetails.JoinDate,
                    Gender = employeedetails.Gender,
                    AddressLine = employeedetails.Addressline,
                    CountryId = employeedetails.CountryId,
                    StateId = employeedetails.StateId,
                    CityId = employeedetails.CityId
                };
                var CountryId = employeedetails.CountryId;
                var stateId = employeedetails.StateId;
                var CityId = employeedetails.CityId;
                List<Country> country = _service.GetCountryList();
                //country.Insert(0, new Country { Id = employeedetails.CountryId, Name = employeedetails.Country.Name });
                List<Department> Dept = _service.GetDepartmentList();
                //Dept.Insert(0, new Department { Id = employeedetails.DepartmentId, Name = employeedetails.Department.Name });
                List<Designation> Des = _service.GetDesignationList();
                //Des.Insert(0, new Designation { Id = employeedetails.DesignationId, Name = employeedetails.Designation.Name });
                var StateList = _service.GetStateList(Convert.ToInt32(CountryId));
                var CityList = _service.GetCityList(Convert.ToInt32(stateId));
                ViewBag.Deptlist = Dept;
                ViewBag.Deslist = Des;
                ViewBag.countrylist = country;
                ViewBag.statelist = StateList;
                ViewBag.citylist = CityList;
                if (employeedetails == null)
                    return View("Not Found");
                else
                    return View("CreateEdit", employeesalaryViewModel);

            }
        }
        /*public IActionResult Create()
        {
            List<Country> country = _service.GetCountryList();
            country.Insert(0, new Country { Id = 0, Name = "---Select Country---" });
            List<Department> Dept = _service.GetDepartmentList();
            Dept.Insert(0, new Department { Id = 0, Name = "---Select Department---" });
            List<Designation> Des = _service.GetDesignationList();
            Des.Insert(0, new Designation { Id = 0, Name = "---Select Designation---" });
            ViewBag.Deptlist = Dept;
            ViewBag.Deslist = Des;
            ViewBag.countrylist = country;
            return View("CreateEdit",new EmployeeSalaryViewModel());
        }
        public ActionResult Edit(int id)
        {
            var employeedetails = _service.GetById(id);
            EmployeeSalaryViewModel employeesalaryViewModel = new EmployeeSalaryViewModel
            {
                Id = employeedetails.Id,
                EmpName = employeedetails.EmpName,
                DesId = employeedetails.DesignationId,
                DeptId = employeedetails.DepartmentId,
                TotalSalary = employeedetails.TotalSalary,
                Age = employeedetails.Age,
                JoinDate = employeedetails.JoinDate,
                Gender = employeedetails.Gender,
                AddressLine = employeedetails.Addressline,
                CountryId = employeedetails.CountryId,
                StateId = employeedetails.StateId,
                CityId = employeedetails.CityId
            };
            List<Country> country = _service.GetCountryList();
            country.Insert(0, new Country { Id = employeedetails.CountryId, Name = employeedetails.Country.Name });
            List<Department> Dept = _service.GetDepartmentList();
            Dept.Insert(0, new Department { Id = employeedetails.DepartmentId, Name = employeedetails.Department.Name });
            List<Designation> Des = _service.GetDesignationList();
            Des.Insert(0, new Designation { Id = employeedetails.DesignationId, Name = employeedetails.Designation.Name });
            ViewBag.Deptlist = Dept;
            ViewBag.Deslist = Des;
            ViewBag.countrylist = country;
            if (employeedetails == null)
                return View("Not Found");
            else
               return View("CreateEdit",employeesalaryViewModel);
            
        }*/
        [HttpPost]
        public JsonResult GetState(string CountryId)
        {
            var StateList = _service.GetStateList(Convert.ToInt32(CountryId));
            StateList.Insert(0, new State { Id = 0, Name = "---Select State---" });
            return Json(new SelectList(StateList, "Id", "Name"));
        }
        [HttpPost]
        public JsonResult GetCity(string StateId)
        {
            var CityList = _service.GetCityList(Convert.ToInt32(StateId));
            CityList.Insert(0, new City { Id = 0, Name = "---Select City---" });
            return Json(new SelectList(CityList, "Id", "Name"));
        }

        [HttpPost]
        public IActionResult CreateEdit(EmployeeSalaryViewModel employee)
        {
            
            if (ModelState.IsValid)
            {
                
                if (employee.Id <= 0)
                {
                    _service.AddEmployeeDetails(employee);
                    return RedirectToAction(nameof(Details));
                }
                // Has Id, therefore it's in database so we update
                else
                {
                    _service.UpdateEmployeeDetails(employee);
                    return RedirectToAction(nameof(Details));
                }
                
            }
            else
            {
                return View(employee);
            }
            

        }
        public IActionResult Details()
        {
            List<Country> country = _service.GetCountryList();
            country.Insert(0, new Country { Id = 0, Name = "---Select Country---" });
            List<Department> Dept = _service.GetDepartmentList();
            Dept.Insert(0, new Department { Id = 0, Name = "---Select Department---" });
            List<Designation> Des = _service.GetDesignationList();
            Des.Insert(0, new Designation { Id = 0, Name = "---Select Designation---" });
            ViewBag.Deptlist = Dept;
            ViewBag.Deslist = Des;
            ViewBag.countrylist = country;
            var employee = _service.GetEmpDetails();
            return View(employee);
        }
        [HttpPost]
        public ActionResult GetSearchRecord(string SearchText,int des,int dept,string gender,int country)
        {
            var list = _service.GetSearchEmpDetails(SearchText,des,dept,gender,country);
            return PartialView("SearchPartial", list);
        }

        [HttpDelete]
        public void Delete(int EmployeeId)
        {
            var employeedetails = _service.GetById(EmployeeId);
            if (employeedetails != null)
            {
                _service.DeleteEmployee(employeedetails);
                
            }
        }

        // Departement CRUD Operation 
        public IActionResult AddEditDepartment(int id)
        {
            if(id<=0)
            {
                return View(new Department());
            }
            else
            {
                var Dept = _service.GetDepartmentListById(id);
                return View(Dept);
            }
        }
        [HttpPost]
        public IActionResult AddEditDepartment(int id,Department Dep,string name)
        {
            if (ModelState.IsValid)
            {
                if (Dep.Id <= 0)
                {
                    bool name1 = _service.dept_exist(Dep.Name);
                    if (name1)
                    {
                        ModelState.AddModelError("Name", "Department  already exist");
                        return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddEditDepartment", Dep) });
                    }
                    else
                    {
                        _service.AddDepartment(Dep);
                        
                    }
                }
                else
                {
                    bool name1 = _service.dept_exist(Dep.Name);
                    if (name1)
                    {
                        ModelState.AddModelError("Name", "Department  already exist");
                        return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddEditDepartment", Dep) });
                    }
                    else
                    {
                        _service.EditDepartment(id, name);
                        
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "ShowDepartementList", _service.GetDepartmentList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddEditDepartment", Dep) });
        }
        /*public IActionResult AddDepartment()
        {
            return View();
        }

        [HttpPost]

        public IActionResult AddDepartment(Department Dep)
        {
            bool name = _service.dept_exist(Dep.Name);
            if(name)
            {
                ModelState.AddModelError("Name", "Department  already exist");
                return View();
            }
            else
            {
                _service.AddDepartment(Dep);
                return RedirectToAction(nameof(Details));
            }
            
        }*/

        public IActionResult ShowDepartementList(string deptsearch,string sortingdept)
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoadDeptData()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                // Skiping number of Rows count  
                var start = Request.Form["start"].FirstOrDefault();
                // Paging Length 10,20  
                var length = Request.Form["length"].FirstOrDefault();
                // Sort Column Name  
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                // Sort Column Direction ( asc ,desc)  
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10,20,50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all Customer data  
                var customerData = _service.GetDepartmentList();
                var customerData1 = _service.GetDepartmentListByquery();
                //var customerData1 = (from dept in _context.Departments select dept);

                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    customerData = customerData1.OrderBy(sortColumn + " " + sortColumnDirection).ToList();
                }
                
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(m => m.Name.Contains(searchValue)).ToList();
                }

                //total number of rows count   
                recordsTotal = customerData.Count();
                //Paging   
                var data = customerData.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data  

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }
        }
        /*public IActionResult EditDepartment(int id)
        {
            var Dept = _service.GetDepartmentListById(id);
            return View(Dept);
        }
        
        
        [HttpPost]
        public IActionResult EditDepartment(int id ,string name)
        {
            _service.EditDepartment(id, name);
            return RedirectToAction(nameof(ShowDepartementList));
        }*/
        [HttpDelete]
        public JsonResult DeleteDepartment(int DepartmentId)
        {
            var result = _service.checkValidDepartment(DepartmentId);
            if (result)
            {
                _service.DeleteDepartment(DepartmentId);
                return Json(new { success = true, responseText = "" });

            }
            else
            {
                return Json(new { success =false, responseText = "Department can not be deleted because Employees working on the Department " });
            }
            
        }

        public IActionResult AddEditDesignation(int id)
        {
            if (id <= 0)
            {
                return View(new Designation()) ;
            }
            else
            {
                var Des = _service.GetDesignationListById(id);
                return View(Des);
            }
        }
        [HttpPost]
        public IActionResult AddEditDesignation(int id, Designation Des, string name)
        {
            if (ModelState.IsValid)
            {
                if (Des.Id <= 0)
                {
                    bool name1 = _service.des_exist(Des.Name);
                    if (name1)
                    {
                        ModelState.AddModelError("Name", "Designation  already exist");
                        return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddEditDesignation", Des) });
                    }
                    else
                    {
                        _service.AddDesignation(Des);
                       
                    }
                }
                else
                {
                    bool name1 = _service.des_exist(Des.Name);
                    if (name1)
                    {
                        ModelState.AddModelError("Name", "Designation  already exist");
                        return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddEditDesignation", Des) });
                    }
                    else
                    {
                        _service.EditDesignation(id, name);
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "ShowDesignationList", _service.GetDesignationList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddEditDesignation", Des) });
        }
        [HttpPost]
        public IActionResult LoadDesData()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                // Skiping number of Rows count  
                var start = Request.Form["start"].FirstOrDefault();
                // Paging Length 10,20  
                var length = Request.Form["length"].FirstOrDefault();
                // Sort Column Name  
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                // Sort Column Direction ( asc ,desc)  
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10,20,50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all Customer data  
                var customerData = _service.GetDesignationList();
                var customerData1 = _service.GetDesignationListByquery();
                //var customerData1 = (from des in _context.Designations select des);

                //Sorting  
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    customerData = customerData1.OrderBy(sortColumn + " " + sortColumnDirection).ToList();
                }

                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(m => m.Name.Contains(searchValue)).ToList();
                }

                //total number of rows count   
                recordsTotal = customerData.Count();
                //Paging   
                var data = customerData.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data  

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }
        }
        public IActionResult ShowDesignationList()
        {
            return View();
        }

        [HttpDelete]
        public JsonResult DeleteDesignation(int DesignationId)
        {
            var result = _service.checkValidDesignation(DesignationId);
            if (result)
            {
                _service.DeleteDesignation(DesignationId);
                return Json(new { success = true, responseText = "" });
            }
            else
            {
                return Json(new { success = false, responseText = "Designation can not be deleted because Employees Curretly have this Designation " });
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
