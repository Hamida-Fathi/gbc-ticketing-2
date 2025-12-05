using GBC_TicketingSystem_2.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GBC_TicketingSystem_2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ApplicationDbContext context, ILogger<DashboardController> logger)
        {
            _context = context;
            _logger = logger;
        }

        
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Admin accessed dashboard.");

            ViewBag.TotalEvents = await _context.Events.CountAsync();
            ViewBag.TotalCategories = await _context.Categories.CountAsync();
            ViewBag.TotalTicketsSold = await _context.Tickets.CountAsync();

           
            ViewBag.UpcomingEvents = await _context.Events
                .Where(e => e.EventDate > DateTime.UtcNow)
                .OrderBy(e => e.EventDate)
                .Take(5)
                .ToListAsync();

          
            ViewBag.TicketsPerEvent = await _context.Tickets
                .GroupBy(t => t.EventId)
                .Select(g => new
                {
                    EventId = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            return View();
        }
    }
}