using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HackathonApp.Data;
using Microsoft.AspNetCore.Identity;

namespace HackathonApp.Helpers
{
    public static class ClaimsHelper
    {
        public static async Task AddClaims(UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            var roles = await userManager.GetRolesAsync(user);
            var existingRole = roles.Single();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, existingRole)
            };

            await userManager.AddClaimsAsync(user,claims);
        }
    }
}