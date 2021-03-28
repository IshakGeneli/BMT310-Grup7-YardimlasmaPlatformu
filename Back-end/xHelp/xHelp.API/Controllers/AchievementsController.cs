using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xHelp.API.Controllers
{
    public class AchievementsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
