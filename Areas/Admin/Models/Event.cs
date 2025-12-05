using System;

namespace GBC_TicketingSystem_2.Areas.Admin.Models
{
    public class Event
    {
        public int EventId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        // ---------- RELATIONSHIP ----------
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        // ---------- TICKET INFO ----------
        public int TicketsAvailable { get; set; }
    }
}