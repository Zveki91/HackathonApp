using System;
using System.Threading.Tasks;
using HackathonApp.Dto;
using HackathonApp.Dto.Purchases;
using HackathonApp.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HackathonApp.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class PurchasesController : BaseController
    {
        private readonly IPurchase _purchase;
        private readonly IContract _contract;
        
        public PurchasesController(IConfiguration configuration, IPurchase purchase, IContract contract) : base(configuration)
        {
            _purchase = purchase;
            _contract = contract;
        }
            
        [HttpPost]
        public async Task<ActionResult<PurchaseDto>> CreatePurchase(CreatePurchaseDto data)
        {
            data.CustomerId = UserId;
            var result = await _purchase.CreatePurchase(data);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<PurchaseDto>> GetPurchaseInfo()
        {
            var result = await _purchase.GetListOfPurchasesForUser(UserId);
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseDto>> GetPurchaseInfo(Guid id)
        {
            var result = await _purchase.GetPurchase(id, UserId);
            return Ok(result);
        }
        
        [HttpGet("company/{id}")]
        public async Task<ActionResult<PurchaseDto>> GetPurchasesForCompany(Guid id)
        {
            var result = await _purchase.GetListOfPurchasesForCompany(id);
            return Ok(result);
        }
    }
}