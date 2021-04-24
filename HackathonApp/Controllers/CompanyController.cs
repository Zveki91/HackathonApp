using System;
using System.Threading.Tasks;
using HackathonApp.Dto;
using HackathonApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HackathonApp.Controllers
{
    [Route("api/[controller]")]
    public class CompanyController : BaseController
    {
        private readonly ICompanies _companies;
        
        public CompanyController(IConfiguration configuration, ICompanies companies) : base(configuration)
        {
            _companies = companies;
        }

        [HttpPost("manager")]
        public async Task<ActionResult<int>> CreateManager(CreateManagerDto data)
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
    }
}