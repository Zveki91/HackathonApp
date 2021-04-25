using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HackathonApp.Dto;
using HackathonApp.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HackathonApp.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class CompaniesController : BaseController
    {
        private readonly ICompanies _companies;
        private readonly IPurchase _purchase;
        
        public CompaniesController(IConfiguration configuration, ICompanies companies) : base(configuration)
        {
            _companies = companies;
        }
        
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateCompany(CreateCompanyDto data)
        {
            var result = await _companies.CreateCompany(data);
            return Ok(result);
        }

        [HttpPost("managers")]
        public async Task<ActionResult<Guid>> CreateManager(CreateManagerDto data)
        {
            try
            {
                var result = await _companies.CreateManager(data);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost("branch-managers")]
        public async Task<ActionResult<Guid>> CreateBranchManager(CreateManagerDto data)
        {
            var result = await _companies.CreateBranchManager(data);
            return Ok(result);
        }

        [HttpPost("branches")]
        public async Task<ActionResult<Guid>> CreateBranch(CreateBranchDto data)
        {
            var result = await _companies.CreateBranch(data);
            return Ok(result);
        }
        

        [HttpGet("{companyId:guid}/employees")]
        public async Task<ActionResult<List<UserDetailsDto>>> GetEmployees(Guid companyId)
        {
            var result = await _companies.GetListOfEmployees(companyId);
            return Ok(result);
        }

        [HttpGet("{companyId:guid}")]
        public async Task<ActionResult<CompanyDto>> GetCompanyInfo(Guid companyId)
        {
            var result = await _companies.GetCompanyData(companyId);
            return Ok(result);
        }

        [HttpGet("{companyId:guid}/transactions")]
        public async Task<ActionResult<object>> GetTransactionsOfCompany(Guid companyId)
        {
            var result = await _purchase.GetListOfPurchasesForCompany(companyId);
            return Ok(null);
        }

        [HttpPost("create-discount")]
        public async Task<ActionResult<DiscountDto>> CreateDiscount(CreateDiscountDto data)
        {
            var result = await _companies.CreateDiscount(data);
            return Ok(result);
        }

        [HttpGet("discounts")]
        public async Task<ActionResult<DiscountDto>> GetDiscounts()
        {
            var result = await _companies.GetListOfDiscounts();
            return Ok(result);
        }
        
        [HttpGet("discounts/{id}")]
        public async Task<ActionResult<DiscountDto>> GetDiscount(Guid id)
        {
            var result = await _companies.GetDiscount(id);
            return Ok(result);
        }
        

    }
}