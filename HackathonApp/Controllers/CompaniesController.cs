using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HackathonApp.Dto;
using HackathonApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HackathonApp.Controllers
{
    [Route("api/[controller]")]
    public class CompaniesController : BaseController
    {
        private readonly ICompanies _companies;
        
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
        
        
    }
}