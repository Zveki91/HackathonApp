using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HackathonApp.Data
{
    public class Purchase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public virtual Branch Branch { get; set; }
        
        public virtual ApplicationUser Customer { get; set; }
        
        public virtual List<Article> Articles { get; set; }
        
        public decimal TotalPrice { get; set; }
    }
}