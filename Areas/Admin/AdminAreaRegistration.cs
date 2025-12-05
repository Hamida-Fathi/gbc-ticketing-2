using Microsoft.AspNetCore.Mvc;

namespace GBC_TicketingSystem_2.Areas.Admin
{
    public class AdminAreaRegistration : AreaAttribute
    {
        public AdminAreaRegistration() : base("Admin") { }
    }
}