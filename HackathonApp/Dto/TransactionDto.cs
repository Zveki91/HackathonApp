using System;
using HackathonApp.Data;

namespace HackathonApp.Dto
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        
        public int Amount { get; set; }
        
        public string Wallet { get; set; }
        
        public DateTime Date { get; set; }
        
        public virtual ApplicationUser User { get; set; }
        
    }
}