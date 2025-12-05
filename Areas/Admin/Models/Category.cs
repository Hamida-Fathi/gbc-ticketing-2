using System.ComponentModel.DataAnnotations;

namespace GBC_TicketingSystem_2.Areas.Admin.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        // OPTIONAL: Navigation property (not required but useful)
        public ICollection<Event>? Events { get; set; }
    }
}