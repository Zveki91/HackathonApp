using System;

namespace HackathonApp.Dto
{
    public class PurchaseArticleDto
    {
        public Guid ArticleId { get; set; }
        
        public int Quantity { get; set; }
    }
}