using Microsoft.AspNetCore.Mvc;

namespace Agency.Areas.admin.Controllers
{
    [Area("Admin")]
    public class PortfolioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
