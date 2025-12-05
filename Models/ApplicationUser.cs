using Microsoft.AspNetCore.Identity;

namespace GBC_TicketingSystem_2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? ProfilePictureUrl { get; set; }
    }

}