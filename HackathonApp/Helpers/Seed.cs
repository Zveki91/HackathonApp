using System;
using System.Threading.Tasks;
using HackathonApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HackathonApp.Helpers
{
    public class Seed
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public Seed(DataContext context, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _context = context;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }


        public async Task SeedDb()
        {
            #region Roles

            RoleManager<Role> roleManager = _serviceProvider.GetRequiredService<RoleManager<Role>>();
            UserManager<ApplicationUser> userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = {"Customer", "Employee", "Administrator"};
            IdentityResult roleResult;

            // Create the roles and seed them to the database, if they don't already exist
            foreach (string roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new Role(roleName));
                }
            }
            
            #endregion
        }
    }
}