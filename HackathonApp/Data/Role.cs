using System;
using Microsoft.AspNetCore.Identity;

namespace HackathonApp.Data
{
    public class Role : IdentityRole<Guid>
    {
        public Role() : base() { }

        public Role(string roleName) : base(roleName)
        {
        }
    }
}