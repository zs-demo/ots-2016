using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebApp.Areas.Workshop.Controllers
{
    [Area("Workshop")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Step1()
        {
            return View();
        }

        public IActionResult Step2()
        {
            return View();
        }

        public IActionResult Step3()
        {
            return View();
        }

        public IActionResult Step4()
        {
            return View();
        }

        public IActionResult Step5()
        {
            return View();
        }
    }
}
