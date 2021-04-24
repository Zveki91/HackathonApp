using System.Collections.Generic;
using System.Threading.Tasks;
using HackathonApp.Dto;
using HackathonApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HackathonApp.Controllers
{
    public class BalancesController : BaseController
    {
        public BalancesController(IConfiguration configuration) : base(configuration)
        {
        }

        private readonly IBalance _balance;

        [HttpGet("tokens-current")]
        public async Task<ActionResult<int>> GetAmountOfTokens()
        {
            var result = await  _balance.GetAmountOfTokens(UserId);
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
    }
}