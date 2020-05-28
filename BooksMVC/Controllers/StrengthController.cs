using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BooksMVC.Controllers
{
    public class StrengthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
