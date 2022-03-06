
using Helperland.Data;
using Helperland.Models;
using Helperland.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Helperland.Controllers
{
    public class HomeController : Controller
    {

        public static int cnt = 0;
        public static int check_mysetting = 0;
        public static int pass_success = 0;
        public static int address_change = 0;
        public static int dashbord = 0;
        private readonly ILogger<HomeController> _logger;
        private readonly HelperlandContext _helperlandContext;

        BookServiceViewModel userAddresses = new BookServiceViewModel();
        MySettingViewModel mySettingViewModel = new MySettingViewModel();

        public HomeController(ILogger<HomeController> logger,HelperlandContext helperlandContext)
        {
            _logger = logger;
            _helperlandContext = helperlandContext;
        }

        
        public IActionResult Index()
        {
            if(Request.Cookies["Email"]!=null && Request.Cookies["Password"]!=null)
            {
                ViewBag.message_mail = Request.Cookies["Email"];
                ViewBag.message_pass = Request.Cookies["Password"];
            }
            return View();
        }

        public IActionResult About()
        {
            
            return View();
        }

        public IActionResult Index_Login()
        {
            ViewBag.modal = string.Format("invalid");
            return View("~/Views/Home/Index.cshtml");
        }
        public IActionResult Contact()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Contact(ContactUsViewModel contactUsViewModel)
        {
            ContactU contactU = new ContactU()
            {
                Name = contactUsViewModel.FirstName + " " + contactUsViewModel.LastName,
                Email = contactUsViewModel.Email,
                Subject = contactUsViewModel.Subject,
                PhoneNumber = contactUsViewModel.Phonenumber,
                Message = contactUsViewModel.Message,
                CreatedOn = DateTime.Now
            };
            //Debug.WriteLine("this is " + contactU.Name);
            _helperlandContext.ContactUs.Add(contactU);
            _helperlandContext.SaveChanges();
            return RedirectToAction("Contact");
        }
        public IActionResult FAQ()
        {
            
            return View();
        }

        public IActionResult Price()
        {
            
            return View();
        }
        public IActionResult CustomerRegistration()
        {
            CustomerRegistrationViewModel customerRegistrationViewModel = new CustomerRegistrationViewModel();
            return View();
        }
        [HttpPost]
        public IActionResult CustomerRegistration(CustomerRegistrationViewModel customerRegistrationViewModel)
        {
            var email_check = email_exist(customerRegistrationViewModel.Email);
            if(email_check)
            {
                ModelState.AddModelError("Email", "Email already exist");
                return View();
            }
            else
            {
                User user = new User()
                {
                    FirstName = customerRegistrationViewModel.FirstName,
                    LastName = customerRegistrationViewModel.LastName,
                    Email = customerRegistrationViewModel.Email,
                    Mobile = customerRegistrationViewModel.Phonenumber,
                    Password = customerRegistrationViewModel.Password,

                    UserTypeId = 1,
                    IsRegisteredUser = true,
                    WorksWithPets = false,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = 0,
                    IsApproved = true,
                    IsActive = true,
                    IsDeleted = false
                };
                _helperlandContext.Users.Add(user);
                _helperlandContext.SaveChanges();
                ViewBag.successModal = string.Format("valid");
                return View("~/Views/Home/Index.cshtml");
            }
        }

       public IActionResult BecomeProvider()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult BecomeProvider(CustomerRegistrationViewModel customerRegistrationViewModel)
        {
            var email_check = email_exist(customerRegistrationViewModel.Email);
            if (email_check)
            {
                ModelState.AddModelError("Email", "Email already exist");
                return View();
            }
            else
            {
                User user = new User()
                {
                    FirstName = customerRegistrationViewModel.FirstName,
                    LastName = customerRegistrationViewModel.LastName,
                    Email = customerRegistrationViewModel.Email,
                    Mobile = customerRegistrationViewModel.Phonenumber,
                    Password = customerRegistrationViewModel.Password,

                    UserTypeId = 2,
                    IsRegisteredUser = true,
                    WorksWithPets = false,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = 0,
                    IsApproved = false,
                    IsActive = true,
                    IsDeleted = false
                };
                _helperlandContext.Users.Add(user);
                _helperlandContext.SaveChanges();
                ViewBag.successModal = string.Format("valid");
                return View("~/Views/Home/BecomeProvider.cshtml");
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel,IFormCollection fc)
        {
            if(ModelState.IsValid)
            {
                var details = (from userlist in _helperlandContext.Users
                               where userlist.Email == loginViewModel.Email && userlist.Password == loginViewModel.Password
                               select new
                               {
                                   userlist.UserId,
                                   userlist.FirstName,
                                   userlist.UserTypeId,
                                   userlist.Email
                               }).ToList();
                if(details.FirstOrDefault()!=null)
                {
                    HttpContext.Session.SetString("UserId",
                                                  details.FirstOrDefault().UserId.ToString());
                    HttpContext.Session.SetString("FirstName", details.FirstOrDefault().FirstName);
                    HttpContext.Session.SetString("UserTypeId", details.FirstOrDefault().UserTypeId.ToString());
                    HttpContext.Session.SetString("Email", details.FirstOrDefault().Email);
                    if(loginViewModel.remember == true)
                    {
                        CookieOptions options = new CookieOptions();
                        options.Expires = DateTime.Now.AddDays(30);
                        Response.Cookies.Append("Email", fc["Email"], options);
                        Response.Cookies.Append("Password", fc["Password"], options);
                    }
                    else
                    {
                        Response.Cookies.Delete("Email");
                        Response.Cookies.Delete("Password");
                    }
                    HttpContext.Session.SetString("loggedIn", "yes");
                    HttpContext.Session.SetString("UserName", details.FirstOrDefault().FirstName);
                    
                    return View("Index");
                    
                    
                }
                else
                {
                    
                    ModelState.AddModelError("", "Invalid Credentials");
                    ViewBag.modal = string.Format("invalid");
                    ViewBag.logged = null;
                    return View("~/Views/Home/Index.cshtml");
                }
            }
            return View(loginViewModel);

        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            cnt = 0;
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPass(LoginViewModel loginViewModel)
        {
            HttpContext.Session.Clear();
            var email_check = email_exist(loginViewModel.Email);
            
            if (email_check)
            {
                var details = (from userlist in _helperlandContext.Users
                               where userlist.Email == loginViewModel.Email
                               select new
                               {
                                   userlist.UserId,
                                   userlist.FirstName,
                                   userlist.Email,
                                   userlist.Password
                               }).ToList();
                if (details.FirstOrDefault() != null)
                {
                    HttpContext.Session.SetString("UserId",
                                                  details.FirstOrDefault().UserId.ToString());
                    HttpContext.Session.SetString("FirstName", details.FirstOrDefault().FirstName);
                    HttpContext.Session.SetString("Email", details.FirstOrDefault().Email);
                }
            }
            else
            {
                ModelState.AddModelError("Email", "User does not Exist!");
                ViewBag.frgtpass = string.Format("forgot pass");
                return View("~/Views/Home/Index.cshtml");
            }
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(LoginViewModel loginViewModel)
        {
            int id = Int32.Parse(HttpContext.Session.GetString("UserId"));
            User user = _helperlandContext.Users.Where(x => x.UserId == id).FirstOrDefault();
            user.Password = loginViewModel.Password;
            _helperlandContext.Update(user);
            _helperlandContext.SaveChanges();
            ViewBag.changepass = string.Format("chengepass");
            return View("~/Views/Home/Index.cshtml");
        }

        public IActionResult book_service()
        {

            string uid = HttpContext.Session.GetString("UserId");
            if (uid != null)
            {
                string uname = HttpContext.Session.GetString("FirstName");
                ViewBag.Uname = uname;
                ViewBag.login_check = String.Format("loggedin");
                if (cnt != 0)
                {
                    if (HttpContext.Session.GetString("again_called") != "spfound")
                    {
                        HttpContext.Session.SetString("ss_step_2", "notset");
                        ViewBag.foundsp = string.Format("spnotfound");
                        string temp_var = ViewBag.foundsp;
                        Debug.WriteLine("this is viewbag foundsp" + temp_var);
                    }
                    else
                    {
                        ViewBag.foundsp = null;
                        HttpContext.Session.SetString("ss_step_2", "notset");

                    }

                }
                cnt = 1;
                int address_fetch_cnt = getAddress();

                HttpContext.Session.SetInt32("address_fetch_cnt", address_fetch_cnt);

                return View(userAddresses);
            }
            else
            {
                ViewBag.login_before_service = string.Format("please login first");
                return RedirectToAction("Index_Login", "Home");
            }

        }

        public IActionResult mySetting()
        {
            if(address_change != 0)
            {
                ViewBag.add_change = "true";
                address_change = 0;
            }
            if(check_mysetting != 0)
            {
                ViewBag.datasave = "true";
                check_mysetting = 0;
            }
            if (pass_success != 0)
            {
                ViewBag.showPassModal = "yes";
                pass_success = 0;
            }
            if(dashbord != 0)
            {
                ViewBag.showDash = "yes";
                dashbord = 0;
            }
            if (ModelState.IsValid)
            {
                var userId = Int32.Parse(HttpContext.Session.GetString("UserId"));
                User req = _helperlandContext.Users.FirstOrDefault(x=>x.UserId == userId);

                if(req.DateOfBirth != null)
                {
                    var DOB_str = (req.DateOfBirth).ToString();
                    var DOB_day = Int32.Parse(DOB_str.Substring(0, 2));
                    var DOB_month = Int32.Parse(DOB_str.Substring(3, 2));
                    var DOB_year = Int32.Parse(DOB_str.Substring(6, 4));
                    Debug.WriteLine("this is fetched " + DOB_str);

                    ViewBag.DOB_day = DOB_day;
                    ViewBag.DOB_month = DOB_month;
                    ViewBag.DOB_year = DOB_year;
                }
               
                ViewBag.fname = req.FirstName;
                ViewBag.lname = req.LastName;
                ViewBag.email = (req.Email).ToString();
                ViewBag.phone = req.Mobile;
                ViewBag.lang = req.LanguageId;
                ViewBag.fetched_ms_data = "yes";

                ms_get_address();
                get_dashboard_service_request();
                get_servicehistory_services();
                return View(mySettingViewModel);
            }
            else
            {
                ViewBag.fetched_ms_data = null;
            }
            
            return View();
        }

        [HttpPost]
        public IActionResult mySetting(MySettingViewModel mySetting)
        {
            if(ModelState.IsValid)
            {
                var userId = Int32.Parse(HttpContext.Session.GetString("UserId"));
                var user = _helperlandContext.Users.Where(u => u.UserId == userId).FirstOrDefault();
                var DOB_day = mySetting.dob_day;
                var DOB_month = mySetting.dob_month;
                var DOB_year = mySetting.dob_year;
                var DOB_f = DOB_year + "-" + DOB_month + "-" + DOB_day;
                DateTime DOB_final = DateTime.Parse(DOB_f);
                TimeSpan time = new TimeSpan(0, 0, 0);
                DOB_final = DOB_final.Date + time;


                user.FirstName = mySetting.user.FirstName;
                user.LastName = mySetting.user.LastName;
                user.Email = mySetting.user.Email;
                user.Mobile = mySetting.user.Mobile;
                user.LanguageId = mySetting.user.LanguageId;
                user.DateOfBirth = DOB_final;
                _helperlandContext.SaveChanges();
            }
            

            check_mysetting = 1;
            return RedirectToAction("mySetting");
        }


        [HttpPost]
        public IActionResult ChangePass(MySettingViewModel mySetting)
        {

            if(ModelState.IsValid)
            {
                var userId = Int32.Parse(HttpContext.Session.GetString("UserId"));
                var user = _helperlandContext.Users.Where(u => u.UserId == userId).FirstOrDefault();
                if(user.Password == mySetting.pwd)
                {
                    user.Password = mySetting.user.Password;
                    _helperlandContext.SaveChanges();
                    pass_success = 1;
                    Debug.WriteLine("hello is if");
                }
                else
                {
                    pass_success = 0;
                    HttpContext.Session.SetString("oldpass", "not ok");
                    
                }
            }
            return RedirectToAction("mySetting");
        }

        [HttpPost]
        public IActionResult add_new_address(MySettingViewModel mySettingViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = Int32.Parse(HttpContext.Session.GetString("UserId"));
                var email = HttpContext.Session.GetString("Email");
                UserAddress userAddress = new UserAddress()
                {
                    UserId = userId,
                    AddressLine1 = mySettingViewModel.street + " " + mySettingViewModel.hno,
                    City = mySettingViewModel.city,
                    PostalCode = mySettingViewModel.pincode,
                    Mobile = mySettingViewModel.phone,
                    IsDefault = false,
                    IsDeleted = false,
                    Email= email
                };
                _helperlandContext.UserAddresses.Add(userAddress);
                _helperlandContext.SaveChanges();
            }
            address_change = 1;
            return RedirectToAction("mySetting");
        }
        [HttpPost]
        public IActionResult edit_address(MySettingViewModel mySettingViewModel)
        {
            if (ModelState.IsValid)
            {
                string addressline = mySettingViewModel.street + " " + mySettingViewModel.hno;
                
                var user_add = _helperlandContext.UserAddresses.Where(x => x.AddressId == mySettingViewModel.hidden_add_id).FirstOrDefault();

                user_add.AddressLine1 = addressline;
                user_add.City = mySettingViewModel.city;
                user_add.Mobile = mySettingViewModel.phone;
                user_add.PostalCode = mySettingViewModel.pincode;

                _helperlandContext.SaveChanges();

            }
            address_change = 1;
            return RedirectToAction("mySetting");
        }


        [HttpPost]
        public IActionResult delete_address(MySettingViewModel mySettingViewModel)
        {
            
            
                var add = _helperlandContext.UserAddresses.Where(x => x.AddressId == mySettingViewModel.delete_add_id).FirstOrDefault();
                _helperlandContext.UserAddresses.Remove(add);
                _helperlandContext.SaveChanges();
            
            address_change = 1;
            return RedirectToAction("mySetting");
        }

        public IActionResult dashboard()
        {
            dashbord = 1;
            return RedirectToAction("mySetting");
        }

        [HttpPost]
        public IActionResult reschedule_service(MySettingViewModel mySettingViewModel)
        {
            if (ModelState.IsValid)
            {
                int service_id = mySettingViewModel.hidden_service_id;

                var startdate = mySettingViewModel.rescheduled_date;
                DateTime start_date = DateTime.Parse(startdate);
                var starttime = mySettingViewModel.rescheduled_time;
                int s_hr = Int32.Parse(starttime.Substring(0, 2));
                int s_min = Int32.Parse(starttime.Substring(3, 2));
                TimeSpan servicestarttime = new TimeSpan(s_hr, s_min, 0);

                start_date = start_date.Date + servicestarttime;

                var servicerequest = _helperlandContext.ServiceRequests.Where(x => x.ServiceId == service_id).FirstOrDefault();
                servicerequest.ServiceStartDate = start_date;
                _helperlandContext.SaveChanges();
            }
            return RedirectToAction("dashboard");
        }
        [HttpPost]
        public IActionResult cancel_service(MySettingViewModel mySettingViewModel)
        {
            if(ModelState.IsValid)
            {
                int service_id = mySettingViewModel.hidden_delete_service;
                var service = _helperlandContext.ServiceRequests.Where(x => x.ServiceId == service_id).FirstOrDefault();
                
                service.Status = 2;
                
                _helperlandContext.SaveChanges();
            }
            return RedirectToAction("dashboard");
        }

        [HttpPost]
        public IActionResult book_service_call()
        {
            return RedirectToAction("book_service");
        }
        //-----------------------non action methods------------------------------------
        public bool email_exist(string email)
        {
            var isCheck = _helperlandContext.Users.Where(eMail => eMail.Email == email).FirstOrDefault();
            return isCheck != null;
        }

        public int getAddress()
        {
            
            Debug.WriteLine("this methd is called");
            HttpContext.Session.SetString("getaddress", "set");
            var userid = Int32.Parse(HttpContext.Session.GetString("UserId"));
            var addresses = (from uaddress in _helperlandContext.UserAddresses
                             where uaddress.UserId == userid
                             select new AddressViewModel()
                             {
                                 id = uaddress.AddressId,
                                 addressline1 = uaddress.AddressLine1,
                                 city = uaddress.City,
                                 phonenumber = uaddress.Mobile,
                                 postalcode = uaddress.PostalCode
                             }).ToList();

            var last_add_id = (from t in _helperlandContext.UserAddresses
                                       where t.UserId == userid
                                       orderby t.AddressId
                                       select t.AddressId).Last();

            if (addresses.FirstOrDefault() != null)
            {
                
                userAddresses.address = new List<AddressViewModel>();
                foreach (var add in addresses)
                {
                    userAddresses.address.Add(add);
                    
                }
            }
            
            return last_add_id;
        }

        public void ms_get_address()
        {
            var userid = Int32.Parse(HttpContext.Session.GetString("UserId"));
            var addresses = (from uaddress in _helperlandContext.UserAddresses
                             where uaddress.UserId == userid
                             select new UserAddress()
                             {
                                 AddressId = uaddress.AddressId,
                                 AddressLine1 = uaddress.AddressLine1,
                                 City = uaddress.City,
                                 Mobile = uaddress.Mobile,
                                 PostalCode = uaddress.PostalCode
                             }).ToList();
            if(addresses.FirstOrDefault() != null)
            {
                mySettingViewModel.userAddresses = new List<UserAddress>();
                foreach(var add in addresses)
                {
                    mySettingViewModel.userAddresses.Add(add);
                }
            }
        }

        public void get_dashboard_service_request()
        {
            var userid = Int32.Parse(HttpContext.Session.GetString("UserId"));
            var new_services = (from sr in _helperlandContext.ServiceRequests
                                from sre in _helperlandContext.ServiceRequestExtras
                                from sra in _helperlandContext.ServiceRequestAddresses
                                where sr.UserId == userid && sr.Status == 1 && sr.ServiceStartDate > DateTime.Now && sr.ServiceRequestId == sre.ServiceRequestId && sr.ServiceRequestId == sra.ServiceRequestId
                                select new DashboardServiceViewModel()
                                {
                                    service_id = sr.ServiceId,
                                    service_date = sr.ServiceStartDate,
                                    duration = sr.ServiceHours + sr.ExtraHours,
                                    service_amount = sr.TotalCost,
                                    extra_service = sre.ServiceExtraId,
                                    service_address = sra.AddressLine1,
                                    phone = sra.Mobile,
                                    email = sra.Email,
                                    comment = sr.Comments,
                                    has_pet = sr.HasPets
                                }).ToList();
            if(new_services.FirstOrDefault()!=null)
            {
                mySettingViewModel.futureRequests = new List<DashboardServiceViewModel>();
                foreach(var ser in new_services)
                {
                    mySettingViewModel.futureRequests.Add(ser);
                }
            }
        }

        public void get_servicehistory_services()
        {
            var userid = Int32.Parse(HttpContext.Session.GetString("UserId"));
            var old_services = (from sr in _helperlandContext.ServiceRequests
                                from sre in _helperlandContext.ServiceRequestExtras
                                from sra in _helperlandContext.ServiceRequestAddresses
                                where (sr.UserId == userid && sr.ServiceStartDate < DateTime.Now && sr.ServiceRequestId == sre.ServiceRequestId && sr.ServiceRequestId == sra.ServiceRequestId)
                                    || (sr.UserId == userid && sr.Status == 2 && sr.ServiceRequestId == sre.ServiceRequestId && sr.ServiceRequestId == sra.ServiceRequestId)

                                select new ServiceHistoryViewModel()
                                {
                                    service_id = sr.ServiceId,
                                    service_date = sr.ServiceStartDate,
                                    duration = sr.ServiceHours + sr.ExtraHours,
                                    service_amount = sr.TotalCost,
                                    status = sr.Status
                                }).ToList();
            
            if(old_services.FirstOrDefault() != null)
            {
                mySettingViewModel.pastServices = new List<ServiceHistoryViewModel>();
                foreach(var ser in old_services)
                {
                    mySettingViewModel.pastServices.Add(ser);
                }
            }
        }
    }
}
