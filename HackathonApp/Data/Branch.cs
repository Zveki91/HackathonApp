using System;
using System.Collections.Generic;

namespace HackathonApp.Data
{
    public class Branch
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public virtual Company Company { get; set; }
        
        public virtual List<Discount> Discounts { get; set; }
        
        public virtual List<BranchArticles> Articles { get; set; }

        public virtual BranchManager BranchManager { get; set; }
        
        
    }
}