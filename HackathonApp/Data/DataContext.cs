using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HackathonApp.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser, Role , Guid>
    {

        public DataContext(DbContextOptions options): base(options)
        {
            
        }
    }
}