using GBC_TicketingSystem_2.Data;
using GBC_TicketingSystem_2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GBC_TicketingSystem_2.Areas.Guest.Controllers
{
    [Area("Guest")]
    [Authorize]   // Protects ticket pages
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<TicketController> _logger;

        public TicketController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            ILogger<TicketController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

   
        public async Task<IActionResult> MyTickets()
        {
         
            if (User.IsInRole("Admin"))
            {
                _logger.LogWarning("Admin attempted to access MyTickets page.");
                TempData["error"] = "Admins cannot view or buy tickets.";
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                _logger.LogWarning("Unauthenticated user attempted to access MyTickets.");
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            _logger.LogInformation("User {UserId} accessed MyTickets page.", userId);

            var tickets = await _context.Tickets
                .Include(t => t.Event)
                .ThenInclude(e => e.Category)
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.PurchaseDate)
                .ToListAsync();

            return View(tickets);
        }
    }
}
