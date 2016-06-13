using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebApp.Controllers
{
    public class BasicsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AspNetCore()
        {
            return View();
        }

        public IActionResult Structure()
        {
            return View();
        }

        public IActionResult EF7()
        {
            return View();
        }

        public IActionResult EFCore()
        {
            return View();
        }

        public IActionResult TagHelpers()
        {
            return View();
        }

        public IActionResult Areas()
        {
            return View();
        }

        public IActionResult Controllers()
        {
            return View();
        }

        public IActionResult Migrations()
        {
            return View();
        }
    }
}
