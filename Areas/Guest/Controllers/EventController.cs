using GBC_TicketingSystem_2.Data;
using GBC_TicketingSystem_2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GBC_TicketingSystem_2.Areas.Guest.Controllers
{
    [Area("Guest")]
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<EventController> _logger;

        public EventController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            ILogger<EventController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }


 
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Guest is viewing all events.");

            var eventsList = await _context.Events
                .Include(e => e.Category)
                .OrderBy(e => e.EventDate)
                .ToListAsync();

            return View(eventsList);
        }


       
        public async Task<IActionResult> Details(int id)
        {
            _logger.LogInformation("Guest is viewing details for EventId {EventId}", id);

            var evt = await _context.Events
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.EventId == id);

            if (evt == null)
            {
                _logger.LogWarning("EventId {EventId} not found.", id);
                TempData["error"] = "Event not found.";
                return RedirectToAction("Index");
            }

            return View(evt);
        }


       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buy(int id)
        {
            _logger.LogInformation("User attempting ticket purchase for EventId {EventId}", id);

            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                _logger.LogWarning("Unauthenticated user attempted purchase.");
                TempData["error"] = "Please log in to buy tickets.";
                return RedirectToAction("Details", new { id });
            }

            var evt = await _context.Events.FindAsync(id);

            if (evt == null)
            {
                _logger.LogError("Purchase failed — EventId {EventId} not found.", id);
                TempData["error"] = "Event not found.";
                return RedirectToAction("Index");
            }

            if (evt.TicketsAvailable <= 0)
            {
                _logger.LogWarning("EventId {EventId} is sold out.", id);
                TempData["error"] = "No tickets available!";
                return RedirectToAction("Details", new { id });
            }

            // Decrease ticket count
            evt.TicketsAvailable--;

            // Create ticket entry
            var ticket = new Ticket
            {
                UserId = userId,
                EventId = id,
                Quantity = 1,
                PurchaseDate = DateTime.UtcNow
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "User {UserId} successfully purchased a ticket for EventId {EventId}",
                userId, id);

            return View("PurchaseSuccess", ticket);
        }
    }
}
