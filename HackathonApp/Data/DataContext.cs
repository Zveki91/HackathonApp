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
        
        public DbSet<Branch> Branch { get; set; }

        public DbSet<Company> Company { get; set; }

        public DbSet<CompanyService> CompanyService { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<Article> Article { get; set; }
        
        public DbSet<BranchManager> BranchManager { get; set; }
        
        public DbSet<Manager> Manager { get; set; }
        
        public DbSet<Discount> Discount { get; set; }
        
        public DbSet<Purchase> Purchase { get; set; }
    }
}