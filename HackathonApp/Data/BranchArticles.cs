using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HackathonApp.Data
{
    public class BranchArticles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        [ForeignKey("BranchId")]
        public virtual Branch Branch { get; set; }
        
        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }
    }
}