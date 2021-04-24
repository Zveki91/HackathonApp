using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HackathonApp.Data
{
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Address { get; set; }

        public virtual List<CompanyService> CompanyServices { get; set; }
        
        public virtual List<Branch> Branches { get; set; }
        
        public virtual Manager Manager { get; set; }
        
        public virtual List<ApplicationUser> Employees { get; set; }
    }
}