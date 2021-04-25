using System;
using HackathonApp.Data;

namespace HackathonApp.Dto
{
    public class DiscountDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ArticleName { get; set; }
        
        public string BranchName { get; set; }
        
        public int PriceReduction { get; set; }
    }
}