using Autokereskedes.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Autokereskedes.Data;
using Microsoft.EntityFrameworkCore;

namespace Autokereskedes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var latestAutos = await _context.Autos
               .OrderBy(a => a.Id)
               .Take(5)
               .ToListAsync();

            return View(latestAutos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
