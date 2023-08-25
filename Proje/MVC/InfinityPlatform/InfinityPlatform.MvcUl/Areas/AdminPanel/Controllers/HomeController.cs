using InfinityPlatform.MvcUl.Areas.AdminPanel.Filters;
using Microsoft.AspNetCore.Mvc;

namespace InfinityPlatform.MvcUl.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [SessionControlAspect]
    public class HomeController : Controller
    {
        [LogAspect]
        public IActionResult Index()
        {
            return View();
        }


    }
}
