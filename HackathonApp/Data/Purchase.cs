using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HackathonApp.Data
{
    public class Purchase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        public virtual Branch Branch { get; set; }
        
        public virtual ApplicationUser Customer { get; set; }
        
        public virtual List<ArticlePurchase> Articles { get; set; }
        
        public decimal TotalPrice { get; set; }
        
        public int TokenAmount { get; set; }
    }
}