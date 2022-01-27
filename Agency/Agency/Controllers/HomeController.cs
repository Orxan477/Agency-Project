using Microsoft.AspNetCore.Mvc;

namespace Agency.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}