using InfinityPlatform.MvcUl.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InfinityPlatform.MvcUl.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}