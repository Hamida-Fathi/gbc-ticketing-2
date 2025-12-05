using GBC_TicketingSystem_2.Areas.Admin.Models;
using GBC_TicketingSystem_2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GBC_TicketingSystem_2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // ------- ADMIN MODELS -------
        public DbSet<Category> Categories { get; set; }
        public DbSet<Event> Events { get; set; }

        // ------- GUEST MODELS -------
        public DbSet<Ticket> Tickets { get; set; }
        // public DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}