using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HackathonApp.Data
{
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        public virtual Category Category { get; set; }
        
        public string Name { get; set; }
        
        public decimal Price { get; set; }
        
        public string ImageUrl { get; set; }
        
        public bool Domestic { get; set; }
    }
}