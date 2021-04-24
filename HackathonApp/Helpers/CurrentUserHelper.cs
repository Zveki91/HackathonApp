using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HackathonApp.Data;
using HackathonApp.Dto;
using HackathonApp.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HackathonApp.Helpers
{
    public static class CurrentUserHelper
    {

        public static async Task<UserDetailsDto> GetUserDetails(DataContext context, UserManager<ApplicationUser> userManager,Guid id, string role)
        {
            var company = await context.Company
                .Include(x => x.Employees)
                .FirstOrDefaultAsync(x => x.Employees.Select(e => e.Id)
                    .Contains(id));
            var user = await userManager.FindByIdAsync(id.ToString());
            return new UserDetailsDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.UserName,
                Role = role,
                Company = company?.Name ?? ""
            };

        }
    }
}