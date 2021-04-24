using System.Collections.Generic;

namespace HackathonApp.Data
{
    public class Branch
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public virtual Company Company { get; set; }

        public virtual BranchManager BranchManager { get; set; }
        
        
    }
}