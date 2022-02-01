
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

namespace Helperland.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        private readonly HelperlandContext _helperlandContext;

        public HomeController(ILogger<HomeController> logger,HelperlandContext helperlandContext)
        {
            _logger = logger;
            _helperlandContext = helperlandContext;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ContactUsViewModel contactUsViewModel = new ContactUsViewModel();
            return View(contactUsViewModel);
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


       
    }
}
