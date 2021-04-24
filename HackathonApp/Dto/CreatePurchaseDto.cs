using System;
using System.Collections.Generic;

namespace HackathonApp.Dto
{
    public class CreatePurchaseDto
    {
        public Guid BranchId { get; set; }

        public Guid CustomerId { get; set; }

        public List<PurchaseArticleDto> Articles { get; set; }

        public decimal TotalPrice { get; set; }
    }
}