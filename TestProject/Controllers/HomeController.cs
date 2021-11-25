using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Models;

namespace TestProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

       
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PageEnglish()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PageBelleng()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PageMath()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PageBiol()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PageHis()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PageObsh()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PageRuslen()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PagePhys()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PageChem()
        {
            return View();
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
