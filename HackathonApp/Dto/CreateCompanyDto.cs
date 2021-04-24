#nullable enable
using System;
using System.Collections.Generic;
using HackathonApp.Data;

namespace HackathonApp.Dto
{
    public class CreateCompanyDto
    {
        public string Name { get; set; }
        
        public string Address { get; set; }

        public Guid ManagerId { get; set; }
        
        public List<ApplicationUser>? Employees { get; set; }
        
        
    }
}