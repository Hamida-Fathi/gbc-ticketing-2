using GBC_TicketingSystem_2.Areas.Admin.Models;
using GBC_TicketingSystem_2.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GBC_TicketingSystem_2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EventController> _logger;

        public EventController(ApplicationDbContext context, ILogger<EventController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // ===================== LIST ==========================
        public async Task<IActionResult> Index()
        {
            var eventsList = await _context.Events
                .Include(e => e.Category)
                .OrderBy(e => e.EventDate)
                .ToListAsync();

            return View(eventsList);
        }

        // ===================== CREATE (GET) ===================
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.Name
                }).ToList();

            return View(new Event());
        }

        // ===================== CREATE (POST) ==================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event evt)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories
                    .Select(c => new SelectListItem
                    {
                        Value = c.CategoryId.ToString(),
                        Text = c.Name
                    }).ToList();

                return View(evt);
            }

            // PostgreSQL requires UTC
            evt.EventDate = DateTime.SpecifyKind(evt.EventDate, DateTimeKind.Utc);

            _context.Events.Add(evt);
            await _context.SaveChangesAsync();

            TempData["success"] = "Event created successfully!";
            return RedirectToAction(nameof(Index));
        }

        // ===================== EDIT (GET) =====================
        public async Task<IActionResult> Edit(int id)
        {
            var evt = await _context.Events.FindAsync(id);

            if (evt == null)
                return NotFound();

            ViewBag.Categories = _context.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.Name
                }).ToList();

            return View(evt);
        }

        // ===================== EDIT (POST) =====================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Event evt)
        {
            if (id != evt.EventId)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories
                    .Select(c => new SelectListItem
                    {
                        Value = c.CategoryId.ToString(),
                        Text = c.Name
                    }).ToList();
                return View(evt);
            }

            evt.EventDate = DateTime.SpecifyKind(evt.EventDate, DateTimeKind.Utc);

            _context.Events.Update(evt);
            await _context.SaveChangesAsync();

            TempData["success"] = "Event updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        // ===================== DELETE (GET) =====================
        public async Task<IActionResult> Delete(int id)
        {
            var evt = await _context.Events
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.EventId == id);

            if (evt == null)
                return NotFound();

            return View(evt);
        }

        // ===================== DELETE (POST) =====================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evt = await _context.Events.FindAsync(id);

            if (evt == null)
                return NotFound();

            bool hasTickets = await _context.Tickets.AnyAsync(t => t.EventId == id);

            if (hasTickets)
            {
                TempData["error"] = "Cannot delete: Tickets have been sold for this event.";
                return RedirectToAction(nameof(Index));
            }

            _context.Events.Remove(evt);
            await _context.SaveChangesAsync();

            TempData["success"] = "Event deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
