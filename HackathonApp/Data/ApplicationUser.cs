using System;
using Microsoft.AspNetCore.Identity;

namespace HackathonApp.Data
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string Wallet { get; set; }
    }
}