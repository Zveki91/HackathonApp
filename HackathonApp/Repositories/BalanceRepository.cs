using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackathonApp.Data;
using HackathonApp.Dto;
using HackathonApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace HackathonApp.Repositories
{
    [Authorize]
    public class BalanceRepository : IBalance
    {
        private readonly IContract _contract;
        private readonly DataContext _context;

        public BalanceRepository(IContract contract, DataContext context)
        {
            _contract = contract;
            _context = context;
        }

        public async Task<int> GetAmountOfTokens(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var balance = await _contract.BalanceOf(user.Wallet);
            return (int)balance;
        }

        public async Task<decimal> GetTokenValue(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var balance = await _contract.BalanceOf(user.Wallet);
            return (decimal)balance * (decimal) 0.04;
        }

        public async Task<int> GetLastIncome(Guid userId)
        {
            var lastTx = await _context.Purchase
                .Include(x => x.Customer)
                .Where(x => x.Customer.Id == userId)
                .OrderByDescending(x => x.Date)
                .Take(1)
                .ToListAsync();
            return lastTx[0].TokenAmount;
        }

        public async Task<int> GetAmountOfSpentTokens(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var transactions = await _context.Purchase
                .Include(x => x.Customer)
                .Where(x => x.Customer.Id == userId)
                .ToListAsync();
            var amountOfSpentTokens = transactions.Sum(x => x.TokenAmount);
            return amountOfSpentTokens;
        }

        public async Task<List<TransactionDto>> GetListOfTransactions(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var transactions = await _context.Purchase
                .Include(x => x.Customer)
                .Where(x => x.Customer.Id == userId)
                .OrderByDescending(x => x.Date)
                .Take(5)
                .ToListAsync();
            var lastTxs = transactions.Select(x => new TransactionDto
            {
                Id = x.Id,
                Amount = x.TokenAmount,
                Wallet = x.Customer.Wallet,
                Date = x.Date,
                User = user
            }).ToList();
            return lastTxs;
        }

        public async Task<List<TransactionDto>> GetListOfLastTransactions(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var transactions = await _context.Purchase
                .Include(x => x.Customer)
                .Where(x => x.Customer.Id == userId)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
            var lastTxs = transactions.Select(x => new TransactionDto
            {
                Id = x.Id,
                Amount = x.TokenAmount,
                Wallet = x.Customer.Wallet,
                Date = x.Date,
                User = user
            }).ToList();
            return lastTxs;
        }

        public async Task<List<DailyTokens>> GetAmountOfTokensEarnedPerDay(Guid userId)
        {
            var transactions = await _context.Purchase
                .Include(x => x.Customer)
                .Where(x => x.Customer.Id == userId && x.Date.Month == DateTime.Now.Month)
                .GroupBy(x => x.Date.Day,(key, e)=> new DailyTokens
                {
                    Day = key,
                    Tokens = e.Sum(t => t.TokenAmount)
                })
                .ToListAsync();
            return transactions;
        }
    }
}