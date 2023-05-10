using FrontToBack.DAL;
using FrontToBack.Models;
using FrontToBack.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrontToBack.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public HomeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IActionResult> Index()
        {

            HomeVM homeVM = new HomeVM()
            {
                Sliders = await _appDbContext.Sliders.ToListAsync(),
                Categories = await _appDbContext.Categories.ToListAsync(),
                Services = await _appDbContext.Services
                .Include(s => s.Category)
                .Include(s => s.ServiceImages)
                .OrderBy(s => s.Id)
                .Take(8)
                .ToListAsync()
            };

            return View(homeVM);
        }
       
    }
}
