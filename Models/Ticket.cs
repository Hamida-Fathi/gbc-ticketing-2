using System;
using System.ComponentModel.DataAnnotations;
using GBC_TicketingSystem_2.Areas.Admin.Models;

namespace GBC_TicketingSystem_2.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }

        // EVENT RELATIONSHIP
        public int EventId { get; set; }
        public Event Event { get; set; }

        // USER RELATIONSHIP
        [Required]
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; }

        // NUMBER OF TICKETS BOUGHT
        public int Quantity { get; set; } = 1;

        // WHEN THE TICKET WAS BOUGHT
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
    }
}