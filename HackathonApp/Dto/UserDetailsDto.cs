using System;

namespace HackathonApp.Dto
{
    public class UserDetailsDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }

        public string Role { get; set; }
        
        public string? Company { get; set; }
    }
}