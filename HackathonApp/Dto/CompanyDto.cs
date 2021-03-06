using System;
using System.Collections.Generic;

namespace HackathonApp.Dto
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public List<UserDetailsDto> Employees { get; set; }
        
        public string ManagerName { get; set; }
        
        public List<BranchDetailsDto> Branches { get; set; }
    }
}