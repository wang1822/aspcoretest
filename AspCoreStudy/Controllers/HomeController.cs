using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspCoreStudy.Models;
using Microsoft.AspNetCore.Authorization;

namespace AspCoreStudy.Controllers
{
    public class HomeController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
