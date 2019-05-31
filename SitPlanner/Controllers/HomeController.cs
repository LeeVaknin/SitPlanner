using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SitPlanner.Models;

namespace SitPlanner.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message1"] = "Have you ever dreamed of a tool which will auotomate seating arrangments for you?";
            ViewData["Message2"] = "SeatMe is here to fulfill your dream.";
            ViewData["Message3"] = "SeatMe offers an automated wedding seating arrangements by using an advanced algorithm.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Please feel free to contact us with any feedback or question.";

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
