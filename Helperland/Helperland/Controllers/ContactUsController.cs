using Helperland.Data;
using Helperland.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly HelperlandContext _helperlandContext;

        public ContactUsController(HelperlandContext helperlandContext)
        {
            _helperlandContext = helperlandContext;
        }
        // GET: ContactUsController
        [HttpPost]
        public IActionResult Submit(ContactU contactU)
        {
            Console.WriteLine(contactU);
            _helperlandContext.ContactUs.Add(contactU);
            _helperlandContext.SaveChanges();
            return RedirectToAction("Contact");
        }
    }
}
