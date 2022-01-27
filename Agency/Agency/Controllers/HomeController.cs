using Data.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Agency.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Portfolio.Where(p=>p.IsDeleted==false).ToList());
        }
    }
}