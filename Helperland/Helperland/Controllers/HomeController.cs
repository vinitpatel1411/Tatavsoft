
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
using Newtonsoft.Json;

namespace Helperland.Controllers
{
    public class HomeController : Controller
    {

        public static int cnt = 0;
        public static int check_mysetting = 0;
        public static int pass_success = 0;
        public static int address_change = 0;
        public static int dashbord = 0;
        public static int service_accepted = 0;
        public static int sp_service_cancel = 0;
        public static int sp_service_complete = 0;
        public static int after_rate = 0;
        public static int sp_cust_block = 0;
        public static int admin_service_request = 0;
        public static int do_active = 0;

        private readonly ILogger<HomeController> _logger;
        private readonly HelperlandContext _helperlandContext;

        BookServiceViewModel userAddresses = new BookServiceViewModel();
        MySettingViewModel mySettingViewModel = new MySettingViewModel();
        spMySettingViewModel spMySettingViewModel = new spMySettingViewModel();
        AdminViewModel AdminView = new AdminViewModel();
        public HomeController(ILogger<HomeController> logger, HelperlandContext helperlandContext)
        {
            _logger = logger;
            _helperlandContext = helperlandContext;
        }


        public IActionResult Index()
        {
            if (Request.Cookies["Email"] != null && Request.Cookies["Password"] != null)
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
            if(ModelState.IsValid)
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
            else
            {
                return View();
            }
            
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
                var uid = _helperlandContext.Users.Where(x => x.Email == customerRegistrationViewModel.Email).FirstOrDefault().UserId;
                UserAddress userAddress = new UserAddress()
                {
                    UserId = uid,
                    AddressLine1 = "add street 1",
                    City = "add city",
                    PostalCode = "111111",
                    IsDefault = true,
                    IsDeleted = false,
                    Mobile = customerRegistrationViewModel.Phonenumber,
                    Email = customerRegistrationViewModel.Email
                };

                _helperlandContext.UserAddresses.Add(userAddress);
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
                    IsActive = false,
                    IsDeleted = false,
                    UserProfilePicture = "3"
                };
                _helperlandContext.Users.Add(user);
                _helperlandContext.SaveChanges();
                var uid = _helperlandContext.Users.Where(x => x.Email == customerRegistrationViewModel.Email).FirstOrDefault().UserId;
                UserAddress userAddress = new UserAddress()
                {
                    UserId = uid,
                    AddressLine1 = "add street 1",
                    City = "add city",
                    PostalCode = "111111",
                    IsDefault = true,
                    IsDeleted = false,
                    Mobile = customerRegistrationViewModel.Phonenumber,
                    Email = customerRegistrationViewModel.Email
                };

                _helperlandContext.UserAddresses.Add(userAddress);
                _helperlandContext.SaveChanges();

                
                ViewBag.successModal = string.Format("valid");
                
            }
            return View("~/Views/Home/BecomeProvider.cshtml");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel, IFormCollection fc)
        {
            if (ModelState.IsValid)
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
                if (details.FirstOrDefault() != null)
                {
                    HttpContext.Session.SetString("UserId",
                                                  details.FirstOrDefault().UserId.ToString());
                    HttpContext.Session.SetString("FirstName", details.FirstOrDefault().FirstName);
                    HttpContext.Session.SetInt32("UserTypeId", details.FirstOrDefault().UserTypeId);
                    HttpContext.Session.SetString("Email", details.FirstOrDefault().Email);
                    if (loginViewModel.remember == true)
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
                    if (details.FirstOrDefault().UserTypeId == 3)
                    {
                        return RedirectToAction("Admin");
                    }
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
        public IActionResult ChangePassword(ForgotPasswordViewModel forgot)
        {
            int id = Int32.Parse(HttpContext.Session.GetString("UserId"));
            User user = _helperlandContext.Users.Where(x => x.UserId == id).FirstOrDefault();
            user.Password = forgot.fpassword;
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
                int address_fetch_cnt = 0;
                if (HttpContext.Session.GetString("zipcode") != "nozip" && HttpContext.Session.GetString("zipcode") != null)
                {
                    var check_zip = _helperlandContext.UserAddresses.Where(x => x.UserId == Int32.Parse(uid) && x.PostalCode == HttpContext.Session.GetString("zipcode"));

                    if (check_zip.FirstOrDefault() != null)
                    {
                        address_fetch_cnt = getAddress();
                        HttpContext.Session.SetString("zipcode", "nozip");
                        HttpContext.Session.SetInt32("address_fetch_cnt", address_fetch_cnt);
                        return View(userAddresses);
                    }
                    else
                    {
                        HttpContext.Session.SetInt32("address_fetch_cnt", address_fetch_cnt);
                        return View();
                    }

                }
                else
                {
                    return View();
                }
            }
            else
            {
                ViewBag.login_before_service = string.Format("please login first");
                return RedirectToAction("Index_Login", "Home");
            }

        }

        public IActionResult mySetting()
        {
            if (address_change != 0)
            {
                ViewBag.add_change = "true";
                address_change = 0;
            }
            if (check_mysetting != 0)
            {
                ViewBag.datasave = "true";
                check_mysetting = 0;
            }
            if (pass_success != 0)
            {
                ViewBag.showPassModal = "yes";
                pass_success = 0;
            }
            if (dashbord != 0)
            {
                ViewBag.showDash = "yes";
                dashbord = 0;
            }
            if(after_rate != 0)
            {
                ViewBag.after_rate = "yes";
                after_rate = 0;
            }
            if (ModelState.IsValid)
            {
                var userId = Int32.Parse(HttpContext.Session.GetString("UserId"));
                User req = _helperlandContext.Users.FirstOrDefault(x => x.UserId == userId);

                if (req.DateOfBirth != null)
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
            if (ModelState.IsValid)
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


                user.FirstName = mySetting.my.fname;
                user.LastName = mySetting.my.lname;
                user.Email = mySetting.my.email;
                user.Mobile = mySetting.my.phone;
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

            if (ModelState.IsValid)
            {
                var userId = Int32.Parse(HttpContext.Session.GetString("UserId"));
                var user = _helperlandContext.Users.Where(u => u.UserId == userId).FirstOrDefault();
                if (user.Password == mySetting.pwd)
                {
                    user.Password = mySetting.forgot.fpassword;
                    _helperlandContext.SaveChanges();
                    pass_success = 1;
                    HttpContext.Session.SetString("oldpass", "ok");
                }
                else
                {
                    pass_success = 0;
                    HttpContext.Session.SetString("oldpass", "notok");

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
                    AddressLine1 = mySettingViewModel.myAddress.street + " " + mySettingViewModel.myAddress.hno,
                    City = mySettingViewModel.myAddress.city,
                    PostalCode = mySettingViewModel.myAddress.pincode,
                    Mobile = mySettingViewModel.myAddress.phone,
                    IsDefault = false,
                    IsDeleted = false,
                    Email = email
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
                string addressline = mySettingViewModel.myAddress.street + " " + mySettingViewModel.myAddress.hno;

                var user_add = _helperlandContext.UserAddresses.Where(x => x.AddressId == mySettingViewModel.hidden_add_id).FirstOrDefault();

                user_add.AddressLine1 = addressline;
                user_add.City = mySettingViewModel.myAddress.city;
                user_add.Mobile = mySettingViewModel.myAddress.phone;
                user_add.PostalCode = mySettingViewModel.myAddress.pincode;

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
            
            int service_id = mySettingViewModel.hidden_service_id;
            var startdate = mySettingViewModel.mySettingReschedule.rescheduled_date;
            DateTime start_date = DateTime.Parse(startdate);
            var starttime = mySettingViewModel.mySettingReschedule.rescheduled_time;
            int s_hr = Int32.Parse(starttime.Substring(0, 2));
            int s_min = Int32.Parse(starttime.Substring(3, 2));
            TimeSpan servicestarttime = new TimeSpan(s_hr, s_min, 0);
            start_date = start_date.Date + servicestarttime;
            var servicerequest = _helperlandContext.ServiceRequests.Where(x => x.ServiceId == service_id).FirstOrDefault();
            servicerequest.ServiceStartDate = start_date;
            _helperlandContext.SaveChanges();
            
            return RedirectToAction("dashboard");
        }

        [HttpPost]
        public IActionResult cancel_service(MySettingViewModel mySettingViewModel)
        {
            if (ModelState.IsValid)
            {
                int service_id = mySettingViewModel.hidden_delete_service;
                var service = _helperlandContext.ServiceRequests.Where(x => x.ServiceId == service_id).FirstOrDefault();

                service.Status = 3;

                _helperlandContext.SaveChanges();
            }
            return RedirectToAction("dashboard");
        }

        [HttpPost]
        public IActionResult book_service_call()
        {
            return RedirectToAction("book_service");
        }

        [HttpPost]
        public IActionResult sp_rating(MySettingViewModel mySetting)
        {
            if(ModelState.IsValid)
            {
                var ser_id = mySetting.rate_ser_id;
                var ser_req_id = _helperlandContext.ServiceRequests.Where(x => x.ServiceId == ser_id).FirstOrDefault().ServiceRequestId;
                var rating_ser = _helperlandContext.Ratings.Where(x => x.ServiceRequestId == ser_req_id).FirstOrDefault();
                rating_ser.OnTimeArrival = mySetting.on_time_arrival;
                rating_ser.Friendly = mySetting.freindly;
                rating_ser.QualityOfService = mySetting.qualit_of_service;
                rating_ser.Ratings = (mySetting.on_time_arrival + mySetting.freindly + mySetting.qualit_of_service) / 3;
                rating_ser.Comments = mySetting.feedback;
                _helperlandContext.SaveChanges();
                after_rate = 1;
            }

            return RedirectToAction("mySetting");
        }

        public IActionResult spMySetting()
        {
            ViewBag.spmysetting = "hello";
            if(pass_success == 1)
            {
                ViewBag.showPassModal = "truw";
                pass_success = 0;
            }
            if (service_accepted == 1)
            {
                ViewBag.service_accepted = "true";
                service_accepted = 0;
            }
            if(sp_service_cancel == 1)
            {
                ViewBag.sp_cancel_service = "true";
                sp_service_cancel = 0;
            }
            if (sp_service_complete == 1)
            {
                ViewBag.sp_complete_service = "true";
                sp_service_complete = 0;
            }
            if(sp_cust_block == 1)
            {
                ViewBag.sp_cust_block = "true";
                sp_cust_block = 0;
            }
            if(do_active == 1)
            {
                ViewBag.doactive = "true";
                do_active = 0;
            }
            ViewBag.spMydetail = "true";
            var userId = Int32.Parse(HttpContext.Session.GetString("UserId"));
            User req = _helperlandContext.Users.FirstOrDefault(x => x.UserId == userId);
            UserAddress add = _helperlandContext.UserAddresses.FirstOrDefault(x => x.UserId == userId);
            spMySettingViewModel.detailViewModel = new spMySettingDetailViewModel();
            
            spMySettingViewModel.detailViewModel.Firsname = req.FirstName;
            spMySettingViewModel.detailViewModel.Lastname = req.LastName;
            spMySettingViewModel.Email = req.Email;
            spMySettingViewModel.detailViewModel.phone = req.Mobile;
            spMySettingViewModel.nationalityid = req.NationalityId;
            spMySettingViewModel.is_active = req.IsActive;
            spMySettingViewModel.avatar = req.UserProfilePicture;
            string[] address = add.AddressLine1.Split(' ');
            var house = address[address.Length - 1];
            address = address.Where(val => val != house).ToArray();
            string add_str = string.Join(" ", address);
            spMySettingViewModel.detailViewModel.street = add_str;
            spMySettingViewModel.detailViewModel.house = Int32.Parse(house);
            spMySettingViewModel.detailViewModel.postal = add.PostalCode;
            spMySettingViewModel.detailViewModel.city = add.City;
            if (req.DateOfBirth != null)
            {
                var DOB_str = (req.DateOfBirth).ToString();
                spMySettingViewModel.dob_day = Int32.Parse(DOB_str.Substring(0, 2));
                spMySettingViewModel.dob_month = Int32.Parse(DOB_str.Substring(3, 2));
                spMySettingViewModel.dob_year = Int32.Parse(DOB_str.Substring(6, 4));
                
            }
            ViewBag.sp_gender = req.Gender;

            get_sp_new_service_request();
            get_sp_upcoming_services();

            

            get_sp_service_history();
            get_sp_my_rating();
            get_sp_customers();
            return View(spMySettingViewModel);
        }

        
        public JsonResult GetEvents()
        {
            var uid = Int32.Parse(HttpContext.Session.GetString("UserId"));
            var events = _helperlandContext.ServiceRequests.Where(x => x.Status == 1 && x.ServiceProviderId == uid).ToList();
            if (events.FirstOrDefault() != null)
            {
                return Json(events);
            }
            else
            {
                return Json("Not Found");
            }
            
        }
        [HttpPost]
        public IActionResult spMydetail(spMySettingViewModel spMySetting)
        {
            var userid = Int32.Parse(HttpContext.Session.GetString("UserId"));
            var u = _helperlandContext.Users.Where(x => x.UserId == userid).FirstOrDefault();
            var ua = _helperlandContext.UserAddresses.Where(x => x.UserId == userid).FirstOrDefault();
            u.FirstName = spMySetting.detailViewModel.Firsname;
            u.LastName = spMySetting.detailViewModel.Lastname;
            u.Mobile = spMySetting.detailViewModel.phone;
            u.NationalityId = spMySetting.nationalityid;
            var DOB_day = spMySetting.dob_day;
            var DOB_month = spMySetting.dob_month;
            var DOB_year = spMySetting.dob_year;
            var DOB_f = DOB_year + "-" + DOB_month + "-" + DOB_day;
            DateTime DOB_final = DateTime.Parse(DOB_f);
            TimeSpan time = new TimeSpan(0, 0, 0);
            DOB_final = DOB_final.Date + time;
            u.DateOfBirth = DOB_final;
            u.Gender = spMySetting.gender;
            u.UserProfilePicture = spMySetting.hidden_avatar.ToString();
            ua.AddressLine1 = spMySetting.detailViewModel.street + " " + spMySetting.detailViewModel.house;
            ua.City = spMySetting.detailViewModel.city;
            if(ua.PostalCode != spMySetting.detailViewModel.postal)
            {
                ua.PostalCode = spMySetting.detailViewModel.postal;
                u.ZipCode = spMySetting.detailViewModel.postal;

                var up_services = _helperlandContext.ServiceRequests.Where(x => x.ServiceProviderId == userid && x.Status == 1).ToList();
                if (up_services.FirstOrDefault() != null)
                {
                    foreach (var i in up_services)
                    {
                        if(i.ServiceStartDate > DateTime.Now)
                        {
                            i.ServiceProviderId = null;
                            i.Status = 0;
                        }
                        else
                        {
                            i.ServiceProviderId = null;
                            i.Status = 3;
                        }
                    }
                }
            }
            _helperlandContext.SaveChanges();
            return RedirectToAction("spMySetting");
        }

        [HttpPost]
        public IActionResult spChangepass(spMySettingViewModel mySetting)
        {

            if (ModelState.IsValid)
            {
                var userId = Int32.Parse(HttpContext.Session.GetString("UserId"));
                var user = _helperlandContext.Users.Where(u => u.UserId == userId).FirstOrDefault();
                if (user.Password == mySetting.pwd)
                {
                    user.Password = mySetting.forgot.fpassword;
                    _helperlandContext.SaveChanges();
                    pass_success = 1;
                    HttpContext.Session.SetString("oldpass", "ok");
                }
                else
                {
                    pass_success = 0;
                    HttpContext.Session.SetString("oldpass", "notok");

                }
            }
            return RedirectToAction("spMySetting");
        }

        [HttpPost]
        public IActionResult accept_service(spMySettingViewModel spMySetting)
        {
            var userid = Int32.Parse(HttpContext.Session.GetString("UserId"));
            bool isactive = _helperlandContext.Users.Where(x => x.UserId == userid).FirstOrDefault().IsActive;
            if(isactive)
            {
                var service = _helperlandContext.ServiceRequests.Where(x => x.ServiceId == spMySetting.hidden_nsr_ser_id);
                service.FirstOrDefault().Status = 1;
                service.FirstOrDefault().ServiceProviderId = userid;
                var myrating = _helperlandContext.Ratings.Where(x => x.ServiceRequestId == service.FirstOrDefault().ServiceRequestId);
                myrating.FirstOrDefault().RatingTo = userid;
                //var sp_avg_rate = _helperlandContext.Ratings.Where(x => x.RatingTo == userid).Average(x=>x.Ratings);
                //myrating.FirstOrDefault().Ratings = sp_avg_rate;
                _helperlandContext.SaveChanges();
                service_accepted = 1;
            }
            else
            {
                do_active = 1;
            }
            
            return RedirectToAction("spMySetting");
        }

        [HttpPost]
        public IActionResult sp_cancel_service(spMySettingViewModel spMySetting)
        {
            var service = _helperlandContext.ServiceRequests.Where(x => x.ServiceId == spMySetting.hidden_cancel_ser_id);
            if(service.FirstOrDefault().ServiceStartDate < DateTime.Now)
            {
                service.FirstOrDefault().Status = 3;
            }
            else
            {
                service.FirstOrDefault().ServiceProviderId = null;
                var myrating = _helperlandContext.Ratings.Where(x => x.ServiceRequestId == service.FirstOrDefault().ServiceRequestId);
                myrating.FirstOrDefault().RatingTo = 1;
                myrating.FirstOrDefault().Ratings = 0;
                service.FirstOrDefault().Status = 0;
            }
            
            _helperlandContext.SaveChanges();
            sp_service_cancel = 1;
            HttpContext.Session.SetString("cant_complete_ser", "false");
            return RedirectToAction("spMySetting");
        }

        [HttpPost]
        public IActionResult sp_complete_service(spMySettingViewModel spMySetting)
        {
            var ser = _helperlandContext.ServiceRequests.Where(x => x.ServiceId == spMySetting.hidden_complete_ser_id);
            var userid = Int32.Parse(HttpContext.Session.GetString("UserId"));
            var check_blocked = _helperlandContext.FavoriteAndBlockeds.Where(x => x.UserId == userid).Where(x=>x.TargetUserId == spMySetting.customer_id);
            if(check_blocked == null)
            {
                FavoriteAndBlocked favblock = new FavoriteAndBlocked()
                {
                    UserId  = userid,
                    TargetUserId = spMySetting.customer_id,
                    IsFavorite=false,
                    IsBlocked=false
                };
                _helperlandContext.FavoriteAndBlockeds.Add(favblock);
                _helperlandContext.SaveChanges();
            }
            int duration_hr = 0;
            int duration_min = 0;
            DateTime end_time = ser.FirstOrDefault().ServiceStartDate;
            double duration = Convert.ToDouble(ser.FirstOrDefault().ServiceHours + ser.FirstOrDefault().ExtraHours);
            string dura_str = duration.ToString();
            bool dura_is_decimal = dura_str.Contains(".");
            string[] dura_arr = dura_str.Split(".");
            if (dura_is_decimal)
            {
                duration_hr = Int32.Parse(dura_arr[0]);



                if (Int32.Parse(dura_arr[1]) != 0)
                {
                    duration_min = 30;
                }
                else
                {
                    duration_min = 0;
                }
            }
            else
            {
                duration_hr = Int32.Parse(dura_str);
                duration_min = 0;

            }


            TimeSpan end_time_cal = new TimeSpan(duration_hr, duration_min, 0);

            end_time = end_time + end_time_cal;


            if (DateTime.Now > end_time)
            {
                ser.FirstOrDefault().Status = 2;
                sp_service_complete = 1;
                _helperlandContext.SaveChanges();
            }
            else
            {
                HttpContext.Session.SetString("cant_complete_ser", "true");
            }
            return RedirectToAction("spMySetting");
        }

        [HttpPost]
        public IActionResult unblock_cust(spMySettingViewModel spMySetting)
        {
            var userid = Int32.Parse(HttpContext.Session.GetString("UserId"));
            var cust = _helperlandContext.FavoriteAndBlockeds.Where(x => x.UserId == userid && x.TargetUserId == spMySetting.customer_id).FirstOrDefault();
            cust.IsBlocked = false;
            _helperlandContext.SaveChanges();
            sp_cust_block = 1;
            return RedirectToAction("spMySetting");
        }

        [HttpPost]
        public IActionResult block_cust(spMySettingViewModel spMySetting)
        {
            var userid = Int32.Parse(HttpContext.Session.GetString("UserId"));
            var cust = _helperlandContext.FavoriteAndBlockeds.Where(x => x.UserId == userid && x.TargetUserId == spMySetting.customer_id).FirstOrDefault();
            cust.IsBlocked = true;
            _helperlandContext.SaveChanges();
            sp_cust_block = 1;
            return RedirectToAction("spMySetting");
        }

        public IActionResult spNewServices()
        {
            service_accepted = 1;
            return RedirectToAction("spMySetting");
        }

        //========================== admin============================
        public IActionResult Admin()
        {
            if(admin_service_request == 1)
            {
                ViewBag.admin_service = "on";
                admin_service_request = 0;
            }
            get_admin_users();
            get_admin_service_requests();
            return View(AdminView);
        }

        [HttpPost]
        public IActionResult service_reschedule_admin(AdminViewModel admin)
        {
            

            int ser_id = admin.hidden_ser_id;
            var startdate = admin.c_ser_date;
            DateTime start_date = DateTime.Parse(startdate);
            var starttime = admin.c_ser_time;
            int s_hr = Int32.Parse(starttime.Substring(0, 2));
            int s_min = Int32.Parse(starttime.Substring(3, 2));
            TimeSpan servicestarttime = new TimeSpan(s_hr, s_min, 0);
            start_date = start_date.Date + servicestarttime;


            var service = _helperlandContext.ServiceRequests.Where(x => x.ServiceId == ser_id).FirstOrDefault();
            var ser_req_id = _helperlandContext.ServiceRequests.Where(x => x.ServiceId == ser_id).FirstOrDefault().ServiceRequestId;
            var service_address = _helperlandContext.ServiceRequestAddresses.Where(x => x.ServiceRequestId == ser_req_id).FirstOrDefault();


            service.ServiceStartDate = start_date;
            service_address.AddressLine1 = admin.adminServiceAddress.c_street + " " + admin.adminServiceAddress.c_hno;
            service_address.City = admin.adminServiceAddress.c_city;
            service_address.PostalCode = admin.adminServiceAddress.c_postal;
            _helperlandContext.SaveChanges();
            admin_service_request = 1;
            
            return RedirectToAction("Admin");
        }

        [HttpPost]
        public IActionResult deactivate_user(AdminViewModel admin)
        {
            int u_id = admin.hidden_u_id;
            var user = _helperlandContext.Users.Where(x => x.UserId == u_id).FirstOrDefault();
            user.IsActive = false;
            _helperlandContext.SaveChanges();
            return RedirectToAction("Admin");
        }

        [HttpPost]
        public IActionResult activate_user(AdminViewModel admin)
        {
            int u_id = admin.hidden_u_id;
            var user = _helperlandContext.Users.Where(x => x.UserId == u_id).FirstOrDefault();
            user.IsActive = true;
            _helperlandContext.SaveChanges();
            return RedirectToAction("Admin");
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
            if (addresses.FirstOrDefault() != null)
            {
                mySettingViewModel.userAddresses = new List<UserAddress>();
                foreach (var add in addresses)
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
                                    from mr in _helperlandContext.Ratings
                                    where sr.UserId == userid && (sr.Status == 0 || sr.Status == 1 ) && sr.ServiceStartDate > DateTime.Now && sr.ServiceRequestId == sre.ServiceRequestId && sr.ServiceRequestId == sra.ServiceRequestId
                                            && sr.ServiceRequestId == mr.ServiceRequestId
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
                                        has_pet = sr.HasPets,
                                        sp_rating = _helperlandContext.Ratings.Where(x=>x.RatingTo == (_helperlandContext.Ratings.Where(x=>x.ServiceRequestId == sr.ServiceRequestId).FirstOrDefault().RatingTo)).Average(x=>x.Ratings),
                                        sp_name = _helperlandContext.Users.Where(x=>x.UserId == sr.ServiceProviderId).FirstOrDefault().FirstName +" "
                                                + _helperlandContext.Users.Where(x => x.UserId == sr.ServiceProviderId).FirstOrDefault().LastName
                                    }).ToList();
                if (new_services.FirstOrDefault() != null)
                {
                    mySettingViewModel.futureRequests = new List<DashboardServiceViewModel>();
                    foreach (var ser in new_services)
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
                                from mr in _helperlandContext.Ratings
                                where sr.UserId == userid && sr.Status != 1 && sr.Status != 0 && 
                                        sr.ServiceRequestId == sre.ServiceRequestId && sr.ServiceRequestId == sra.ServiceRequestId &&
                                        sr.ServiceRequestId == mr.ServiceRequestId
                                    

                                select new ServiceHistoryViewModel()
                                {
                                    service_id = sr.ServiceId,
                                    service_date = sr.ServiceStartDate,
                                    duration = sr.ServiceHours + sr.ExtraHours,
                                    service_amount = sr.TotalCost,
                                    status = sr.Status,
                                    sp_rating = _helperlandContext.Ratings.Where(x => x.RatingTo == (_helperlandContext.Ratings.Where(x => x.ServiceRequestId == sr.ServiceRequestId).FirstOrDefault().RatingTo)).Average(x => x.Ratings),
                                    sp_name = _helperlandContext.Users.Where(x => x.UserId == sr.ServiceProviderId).FirstOrDefault().FirstName +
                                            " " + _helperlandContext.Users.Where(x => x.UserId == sr.ServiceProviderId).FirstOrDefault().LastName
                                }).ToList();

            if (old_services.FirstOrDefault() != null)
            {
                mySettingViewModel.pastServices = new List<ServiceHistoryViewModel>();
                foreach (var ser in old_services)
                {
                    mySettingViewModel.pastServices.Add(ser);
                }
            }
            
            
            
        }

        //----------------------service provider methods ----------------------
        public void get_sp_new_service_request()
        {
            var userid = Int32.Parse(HttpContext.Session.GetString("UserId"));
            if (ModelState.IsValid)
            {
                var user_zip = from zip in _helperlandContext.Users
                               where zip.UserId == userid
                               select new
                               {
                                   zip.ZipCode
                               };
                var user_Zipcode = user_zip.FirstOrDefault().ZipCode;
                
                var req = _helperlandContext.ServiceRequests.Where(x => x.ZipCode == user_Zipcode && x.ServiceStartDate > DateTime.Now && x.ServiceProviderId == null && x.Status == 0).ToList();
                if(req.Count() > 0)
                {
                    spMySettingViewModel.newServices = new List<NewServiceRequestViewModel>();
                    foreach (var item in req)
                    {
                        var isBlocked = _helperlandContext.FavoriteAndBlockeds.FirstOrDefault(x => x.UserId == userid && x.TargetUserId == item.UserId && x.IsBlocked == true);
                        
                        if(isBlocked == null)
                        {
                            NewServiceRequestViewModel res = new NewServiceRequestViewModel();
                            res.service_id = item.ServiceId;
                            res.service_date = item.ServiceStartDate;
                            res.duration = item.ServiceHours + item.ExtraHours;
                            res.service_amount = item.TotalCost;
                            res.comment = item.Comments;
                            res.has_pet = item.HasPets;

                            var sre = _helperlandContext.ServiceRequestExtras.FirstOrDefault(x => x.ServiceRequestId == item.ServiceRequestId);
                            res.extra_service = sre.ServiceExtraId;

                            var uid = _helperlandContext.Users.FirstOrDefault(x => x.UserId == item.UserId);
                            res.cust_name = uid.FirstName;


                            var sra = _helperlandContext.ServiceRequestAddresses.FirstOrDefault(x => x.ServiceRequestId == item.ServiceRequestId);
                            res.pincode = item.ZipCode;
                            res.city = sra.City;
                            res.service_address = sra.AddressLine1;
                            res.phone = sra.Mobile;
                            res.email = sra.Email;
                            res.conflictId = null;
                            var check = _helperlandContext.ServiceRequests.Where(x => x.ServiceProviderId == userid && x.ServiceStartDate >DateTime.Now  && x.Status == 1).ToList();
                            if(check.Count() > 0)
                            {
                                foreach(var i in check)
                                {
                                    var newSerStartDT = item.ServiceStartDate;
                                    var newSerEndDT = item.ServiceStartDate.AddHours((double)item.ServiceHours + (double)item.ExtraHours);
                                    var accSerStartDT = i.ServiceStartDate;
                                    var accSerEndDT = i.ServiceStartDate.AddHours((double)i.ServiceHours + (double)i.ExtraHours + 1);

                                    if (newSerStartDT >= accSerStartDT && newSerStartDT <= accSerEndDT)
                                    {
                                        res.conflictId = i.ServiceId;
                                        break;
                                    }
                                    else if (newSerEndDT >= accSerStartDT && newSerEndDT <= accSerEndDT)
                                    {
                                        res.conflictId = i.ServiceId;
                                        break;
                                    }
                                    else if (newSerStartDT < accSerStartDT && newSerEndDT > accSerEndDT)
                                    {
                                        res.conflictId = i.ServiceId;
                                        break;
                                    }
                                    else
                                    {
                                        res.conflictId = null;
                                    }
                                }
                            }
                            else
                            {
                                res.conflictId = null;
                            }

                            spMySettingViewModel.newServices.Add(res);
                        }
                    }
                }

            }
        }
        public void get_sp_upcoming_services()
        {
            var userid = Int32.Parse(HttpContext.Session.GetString("UserId"));
            if (ModelState.IsValid)
            {
                var user_zip = from zip in _helperlandContext.Users
                               where zip.UserId == userid
                               select new
                               {
                                   zip.ZipCode
                               };
                var user_Zipcode = user_zip.FirstOrDefault().ZipCode;
                var up_services = (from sr in _helperlandContext.ServiceRequests
                                    from sre in _helperlandContext.ServiceRequestExtras
                                    from sra in _helperlandContext.ServiceRequestAddresses
                                    from uid in _helperlandContext.Users
                                    where sr.Status == 1 && sr.ZipCode == user_Zipcode && sr.ServiceRequestId == sre.ServiceRequestId && sr.ServiceRequestId == sra.ServiceRequestId && sr.UserId == uid.UserId && sr.ServiceProviderId == userid
                                   select new UpcomingServiceViewModel()
                                    {
                                        cust_id = sr.UserId,
                                        service_id = sr.ServiceId,
                                        service_date = sr.ServiceStartDate,
                                        duration = sr.ServiceHours + sr.ExtraHours,
                                        service_amount = sr.TotalCost,
                                        extra_service = sre.ServiceExtraId,
                                        service_address = sra.AddressLine1,
                                        comment = sr.Comments,
                                        has_pet = sr.HasPets,
                                        cust_first_name = uid.FirstName,
                                        cust_last_name = uid.LastName,
                                        distance = sr.Distance,
                                        pincode = sra.PostalCode,
                                        city = sra.City
                                    }).ToList();
                if(up_services.FirstOrDefault() != null)
                {
                    spMySettingViewModel.upcomingServices = new List<UpcomingServiceViewModel>();
                    foreach(var ser in up_services)
                    {
                        spMySettingViewModel.upcomingServices.Add(ser);
                    }
                }
            }

        }
        public void get_sp_service_history()
        {
            var userid = Int32.Parse(HttpContext.Session.GetString("UserId"));
            var complete_ser = from sr in _helperlandContext.ServiceRequests
                               from sra in _helperlandContext.ServiceRequestAddresses
                               where sr.ServiceProviderId == userid && sr.Status == 2 && sr.ServiceRequestId == sra.ServiceRequestId
                               select new spServiceHistoryViewModel()
                               {
                                   service_id = sr.ServiceId,
                                   cust_name = _helperlandContext.Users.Where(x=>x.UserId == sr.UserId).FirstOrDefault().FirstName + " "
                                                + _helperlandContext.Users.Where(x => x.UserId == sr.UserId).FirstOrDefault().LastName ,
                                   addressline1 = sra.AddressLine1,
                                   pincode = sra.PostalCode,
                                   city = sra.City,
                                   service_datetime = sr.ServiceStartDate,
                                   duration = sr.ServiceHours +sr.ExtraHours
                               };
            if (complete_ser.FirstOrDefault() != null)
            {
                spMySettingViewModel.spServiceHistories = new List<spServiceHistoryViewModel>();
                foreach(var data in complete_ser)
                {
                    spMySettingViewModel.spServiceHistories.Add(data);
                }
            }
        }
        public void get_sp_my_rating()
        {
            var userid = Int32.Parse(HttpContext.Session.GetString("UserId"));
            var my_rating = from mr in _helperlandContext.Ratings
                            from sr in _helperlandContext.ServiceRequests

                            where sr.ServiceProviderId == userid && sr.Status == 2 && mr.RatingTo == userid && sr.ServiceRequestId == mr.ServiceRequestId
                            select new spMyratingViewModel()
                            {
                                service_id = sr.ServiceId,
                                service_datetime = sr.ServiceStartDate,
                                duration = sr.ServiceHours + sr.ExtraHours,
                                rating = (mr.OnTimeArrival + mr.QualityOfService + mr.Friendly) / 3,
                                cust_feedback = mr.Comments,
                                cust_name = _helperlandContext.Users.Where(x => x.UserId == sr.UserId).FirstOrDefault().FirstName + " "
                                                + _helperlandContext.Users.Where(x => x.UserId == sr.UserId).FirstOrDefault().LastName
                            };
            if(my_rating.FirstOrDefault() != null)
            {
                spMySettingViewModel.spMyratings = new List<spMyratingViewModel>();
                foreach(var rating in my_rating)
                {
                    spMySettingViewModel.spMyratings.Add(rating);
                }
            }    

        }
        public void get_sp_customers()
        {
            var userid = Int32.Parse(HttpContext.Session.GetString("UserId"));
            var my_customers = (from favblock in _helperlandContext.FavoriteAndBlockeds
                               from u in _helperlandContext.Users
                               where favblock.UserId == userid && favblock.TargetUserId == u.UserId
                               select new spBlockCustomersViewModel()
                               {
                                   cust_id = favblock.TargetUserId,
                                   cust_name = _helperlandContext.Users.Where(x=>x.UserId == favblock.TargetUserId).FirstOrDefault().FirstName+ " "+
                                                _helperlandContext.Users.Where(x => x.UserId == favblock.TargetUserId).FirstOrDefault().LastName,
                                   blocked = favblock.IsBlocked
                               }).ToList();
            if(my_customers.FirstOrDefault()!=null)
            {
                spMySettingViewModel.spBlockCustomers = new List<spBlockCustomersViewModel>();
                foreach(var cust in my_customers)
                {
                    spMySettingViewModel.spBlockCustomers.Add(cust);
                }
            }
        }

        //-----------------------admin methods---------------------------------
        public void get_admin_users()
        {
            var u = _helperlandContext.Users;
            if (u.FirstOrDefault() != null)
            {
                AdminView.users = new List<User>();
                foreach (var data in u)
                {
                    AdminView.users.Add(data);
                }
            }
        }
        public void get_admin_service_requests()
        {
            var services = from sr in _helperlandContext.ServiceRequests
                           from sra in _helperlandContext.ServiceRequestAddresses
                           from mr in _helperlandContext.Ratings
                           where sr.ServiceRequestId == sra.ServiceRequestId && sr.ServiceRequestId == mr.ServiceRequestId
                           select new AdminServiceRequestViewModel()
                           {
                               ser_id = sr.ServiceId,
                               start_date = sr.ServiceStartDate,
                               duration = sr.ServiceHours + sr.ExtraHours,
                               cust_name = _helperlandContext.Users.Where(x => x.UserId == sr.UserId).FirstOrDefault().FirstName + " "
                                            + _helperlandContext.Users.Where(x => x.UserId == sr.UserId).FirstOrDefault().LastName,
                               addressline1 = sra.AddressLine1,
                               pincode = sra.PostalCode,
                               city = sra.City,
                               sp_id = sr.ServiceProviderId,
                               sp_name = _helperlandContext.Users.Where(x=>x.UserId == sr.ServiceProviderId).FirstOrDefault().FirstName +" "
                                         + _helperlandContext.Users.Where(x => x.UserId == sr.ServiceProviderId).FirstOrDefault().LastName,
                               sp_rating = (mr.OnTimeArrival + mr.QualityOfService + mr.Friendly) / 3,
                               amount = sr.TotalCost,
                               status = sr.Status
                           };
            if(services.FirstOrDefault()!=null)
            {
                
                AdminView.adminServiceRequests = new List<AdminServiceRequestViewModel>();
                foreach(var ser in services)
                {
                    AdminView.adminServiceRequests.Add(ser);
                }
            }
        }
    }
}
