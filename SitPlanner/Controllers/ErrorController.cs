using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SitPlanner.Controllers
{
    [Route("Error")]
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        [HttpGet("/Error/{errorId}")]
        public IActionResult InvalidAction(int errorId)
        {
            if (errorId == 401 || errorId == 403)
                return View("Views/Error/InvalidAction.cshtml");
            if (errorId == 405)
                return View("Views/Error/CreateEventError.cshtml");
            return OurError();
        }

        [Route("")]
        [Route("/Error")]
        public IActionResult OurError()
        {
            return View("Views/Error/OurError.cshtml");
        }

        [Route("/Error/InvalidAction")]
        public IActionResult InvalidAction()
        {
            return View("Views/Error/InvalidAction.cshtml");
        }
    }
}