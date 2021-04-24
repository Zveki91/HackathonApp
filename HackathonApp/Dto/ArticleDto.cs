using System;

namespace HackathonApp.Dto
{
    public class ArticleDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public decimal Price { get; set; }
    }
}