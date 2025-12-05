using GBC_TicketingSystem_2.Data;
using GBC_TicketingSystem_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GBC_TicketingSystem_2.Areas.Guest.Controllers
{
    [Area("Guest")]
    public class PurchaseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseController(ApplicationDbContext context)
        {
            _context = context;
        }

 
        public async Task<IActionResult> Confirmation(int id)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Event)
                .FirstOrDefaultAsync(t => t.TicketId == id);

            if (ticket == null)
                return NotFound();

            return View(ticket);
        }
    }
}