using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DemoWebApp.Data;
using Microsoft.AspNetCore.Identity;
using DemoWebApp.Models;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoWebApp.Areas.FilmFanatic.Controllers
{
    [Area("FilmFanatic")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private ILogger logger;

        public HomeController(ILoggerFactory logFactory, ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.logger = logFactory.CreateLogger<HomeController>();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
