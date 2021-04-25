using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HackathonApp.Dto;
using HackathonApp.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Nethereum.BlockchainProcessing.BlockStorage.Repositories;

namespace HackathonApp.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BalancesController : BaseController
    {
        public BalancesController(IConfiguration configuration, IBalance balance) : base(configuration)
        {
            _balance = balance;
        }

        private readonly IBalance _balance;

        [HttpGet("statistics")]
        public async Task<ActionResult<object>> GetStatistics()
        {
            decimal tokens = await _balance.GetAmountOfTokens(UserId);
            decimal value = await _balance.GetTokenValue(UserId);
            int spent = await _balance.GetAmountOfSpentTokens(UserId);
            int lastIncom = await _balance.GetLastIncome(UserId);
            List<TransactionDto> txs = await _balance.GetListOfTransactions(UserId);
            return Ok(new 
            { 
                TokenAmount = tokens,
                TokenValue = value,
                TokensSpent = spent,
                LastIncome = lastIncom,
                LastTransaction = txs
            });
        }

        [HttpGet("tokens-current")]
        public async Task<ActionResult<int>> GetAmountOfTokens()
        {
            var result = await _balance.GetAmountOfTokens(UserId);
            return Ok(result);
        }
        
        [HttpGet("tokens-value")]
        public async Task<ActionResult<decimal>> GetValueOfTokens()
        {
            var result = await  _balance.GetTokenValue(UserId);
            return Ok(result);
        }

        [HttpGet("last-income")]
        public async Task<ActionResult<int>> GetLastIncome()
        {
            var result = await _balance.GetLastIncome(UserId);
            return Ok(result);
        }

        [HttpGet("tokens-spent")]
        public async Task<ActionResult<int>> GetAmountOfSpentTokens()
        {
            var result = await _balance.GetAmountOfSpentTokens(UserId);
            return Ok(result);
        }

        [HttpGet("transactions")]
        public async Task<ActionResult<List<TransactionDto>>> GetListOfTransactions()
        {
            var result = await _balance.GetListOfTransactions(UserId);
            return Ok(result);
        }
        
        [HttpGet("transactions-last")]
        public async Task<ActionResult<List<TransactionDto>>> GetLastTransactions()
        {
            var result = await _balance.GetListOfLastTransactions(UserId);
            return Ok(result);
        }

        [HttpGet("tokens-daily")]
        public async Task<ActionResult<DailyTokens>> GetDailyTokensReport()
        {
            var result = await _balance.GetAmountOfTokensEarnedPerDay(UserId);
            return Ok(result);
        }
        
        [HttpGet("tokens-monthly")]
        public async Task<ActionResult<DailyTokens>> GetMonthlyTokensReport()
        {
            var result = await _balance.GetAmountOfTokensEarnedPerMonth(UserId);
            return Ok(result);
        }
    }
}