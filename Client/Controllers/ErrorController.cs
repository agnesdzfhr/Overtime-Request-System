using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult NotFound404()
        {
            return View();
        }

        public IActionResult Unauthorized401()
        {
            return View();
        }

        public IActionResult Forbidden403()
        {
            return View();
        }
    }
}
