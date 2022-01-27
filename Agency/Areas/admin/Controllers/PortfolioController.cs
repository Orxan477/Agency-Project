using Agency.Utilities;
using Agency.ViewModels.Product;
using AutoMapper;
using Core.Models;
using Data.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Agency.Areas.admin.Controllers
{
    [Area("Admin")]
    public class PortfolioController : Controller
    {
        private IWebHostEnvironment _env;
        private AppDbContext _context;
        private IMapper _mapper;
        private string _errorMessage;

        public PortfolioController(AppDbContext context, IWebHostEnvironment env,IMapper mapper)
        {
            _env = env;
            _context = context;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View(_context.Portfolio.Where(p=>p.IsDeleted==false).ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM productCreate)
        {
            if (!ModelState.IsValid) return View(productCreate);
            bool isExistName = await _context.Portfolio.AnyAsync(x => x.Name.Trim().ToLower() == productCreate.Name.Trim().ToLower());
            if (isExistName)
            {
                ModelState.AddModelError("Name", "Multiple Name");
                return View(productCreate);
            }
            if (!CheckImageValid(productCreate.Photo, 200,"image/"))
            {
                ModelState.AddModelError("Photo", _errorMessage);
                return View(productCreate);
            }
            string fileName=await Extension.SaveFileAsync(productCreate.Photo, _env.WebRootPath, "assets/img");
            Portfolio portfolio = new Portfolio
            {
                Name=productCreate.Name,
                Category=productCreate.Category,
                Image=fileName,
                Client=productCreate.Client,
                Info=productCreate.Info
            };
            await _context.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool CheckImageValid(IFormFile file,int size, string type)
        {
            if (!Extension.CheckSize(file,size))
            {
                _errorMessage = $"Image size>{200}";
                return false;
            }
            if (!Extension.CheckType(file,type))
            {
                _errorMessage = $"Incorrect type";
                return false;
            }
            return true;
        }
        public IActionResult Update(int id)
        {
            Portfolio dbPortfolio = _context.Portfolio.Where(p => !p.IsDeleted && p.Id == id).FirstOrDefault();
            if (dbPortfolio is null) return NotFound();
            PortfolioUpdateVM portfolio = _mapper.Map<PortfolioUpdateVM>(dbPortfolio);
            return View(portfolio);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, PortfolioUpdateVM updateVM)
        {
            if (!ModelState.IsValid) return View(updateVM);
            Portfolio dbPortfolio = _context.Portfolio.Where(p => !p.IsDeleted && p.Id == id).FirstOrDefault();
            if (dbPortfolio is null) return NotFound();

            bool isExistContext = await _context.Portfolio.AnyAsync(p => p.Name.Trim().ToLower() == updateVM.Name.Trim().ToLower());
            bool isExistCurrent = dbPortfolio.Name.Trim().ToLower() == updateVM.Name.Trim().ToLower();
            if (isExistContext==true && isExistCurrent==false)
            {
                ModelState.AddModelError("Name", "This name currently use");
                return View(updateVM);
            }
            if (!isExistCurrent)
            {
                dbPortfolio.Name = updateVM.Name;
            }
            bool isExistCategory = dbPortfolio.Category.Trim().ToLower() == updateVM.Category.Trim().ToLower();
            if (!isExistCategory)
            {
                dbPortfolio.Category = updateVM.Category;
            }
            bool isExistClient = dbPortfolio.Client.Trim().ToLower() == updateVM.Client.Trim().ToLower();
            if (!isExistClient)
            {
                dbPortfolio.Client = updateVM.Client;
            }
            bool isExistInfo = dbPortfolio.Info.Trim().ToLower() == updateVM.Info.Trim().ToLower();
            if (!isExistInfo)
            {
                dbPortfolio.Info = updateVM.Info;
            }
            if (!CheckImageValid(updateVM.Photo, 200, "image/"))
            {
                ModelState.AddModelError("Photo", _errorMessage);
                return View(updateVM);
            }
            string fileName = await Extension.SaveFileAsync(updateVM.Photo, _env.WebRootPath, "assets/img");
            Helper.RemoveFile(_env.WebRootPath, "assets/img", dbPortfolio.Image);
            dbPortfolio.Image = fileName;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Portfolio dbPortfolio = _context.Portfolio.Where(p => !p.IsDeleted && p.Id == id).FirstOrDefault();
            if (dbPortfolio is null) return NotFound();

            //Helper.RemoveFile(_env.WebRootPath, "assets/img", dbPortfolio.Image);
            //_context.Portfolio.Remove(dbPortfolio);

            dbPortfolio.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
    
}
