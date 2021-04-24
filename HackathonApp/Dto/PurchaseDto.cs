using System;
using System.Collections.Generic;

namespace HackathonApp.Dto
{
    public class PurchaseDto
    {
        public Guid Id { get; set; }
        public Guid BranchId { get; set; }
        public Guid UserId { get; set; }

        public List<ArticleDto> Articles { get; set; }
        
        public decimal TotalPrice { get; set; }

    }
}