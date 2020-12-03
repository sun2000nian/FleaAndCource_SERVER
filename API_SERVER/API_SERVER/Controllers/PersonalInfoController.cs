using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_SERVER.Controllers
{
    public class PersonalInfoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
