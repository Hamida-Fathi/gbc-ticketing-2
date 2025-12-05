using GBC_TicketingSystem_2.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GBC_TicketingSystem_2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var soldTickets = await _context.Tickets
                .Include(t => t.Event)
                .Include(t => t.User)
                .OrderByDescending(t => t.PurchaseDate)
                .ToListAsync();

            return View(soldTickets);
        }
    }
}