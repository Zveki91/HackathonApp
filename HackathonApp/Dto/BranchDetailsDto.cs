using System;

namespace HackathonApp.Dto
{
    public class BranchDetailsDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string ManagerName { get; set; }
        
        public string Address { get; set; }
    }
}