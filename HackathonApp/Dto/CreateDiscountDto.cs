using System;
using HackathonApp.Data;

namespace HackathonApp.Dto
{
    public class CreateDiscountDto
    {

        public string Name { get; set; }

        public Guid ArticleId { get; set; }
        
        public Guid Branch { get; set; }
        
        public int PriceReduction { get; set; }
    }
}