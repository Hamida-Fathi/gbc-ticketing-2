using System.ComponentModel.DataAnnotations;
using GBC_TicketingSystem_2.Areas.Admin.Models;

namespace GBC_TicketingSystem_2.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }

        [Required]
        public int EventId { get; set; }
        public Event Event { get; set; }

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime PurchaseDate { get; set; } = DateTime.Now;
    }
}