using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GBC_TicketingSystem_2.Models;
using GBC_TicketingSystem_2.Data;

namespace GBC_TicketingSystem_2.Controllers
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

        // Home / Landing Page
        public async Task<IActionResult> Index()
        {
            // Load a few upcoming events to show on the home page
            var featuredEvents = await _context.Events
                .Include(e => e.Category)
                .OrderBy(e => e.EventDate)
                .Take(3)
                .ToListAsync();

            ViewBag.FeaturedEvents = featuredEvents;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}