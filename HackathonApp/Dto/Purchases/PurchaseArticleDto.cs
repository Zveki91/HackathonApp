using System;

namespace HackathonApp.Dto.Purchases
{
    public class PurchaseArticleDto
    {
        public Guid ArticleId { get; set; }
        
        public int Quantity { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public decimal? Price { get; set; }
    }
}