using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HackathonApp.Data
{
    public class ArticlePurchase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }
        
        [ForeignKey("PurchaseId")]
        public virtual Purchase Purchase { get; set; }
        
        public int Quantity { get; set; }
        
        public decimal Price { get; set; }
    }
}