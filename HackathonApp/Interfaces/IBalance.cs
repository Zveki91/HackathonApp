using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HackathonApp.Dto;

namespace HackathonApp.Interfaces
{
    public interface IBalance
    {
        Task<decimal> GetAmountOfTokens(Guid userId);

        Task<decimal> GetTokenValue(Guid userId);

        Task<int> GetLastIncome(Guid userId);

        Task<int> GetAmountOfSpentTokens(Guid userId);

        Task<List<TransactionDto>> GetListOfTransactions(Guid userId);

        Task<List<TransactionDto>> GetListOfLastTransactions(Guid userId);

        Task<List<DailyTokens>> GetAmountOfTokensEarnedPerDay(Guid userId);
        Task<List<DailyTokens>> GetAmountOfTokensEarnedPerMonth(Guid userId);
    }
    
}