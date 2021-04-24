using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HackathonApp.Data;
using HackathonApp.Dto;
using HackathonApp.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HackathonApp.Repositories
{
    public class CompanyRepository : ICompanies
    {
        private readonly DataContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CompanyRepository(UserManager<ApplicationUser> userManager, DataContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<int> CreateCompany(CreateCompanyDto data)
        {
            throw new System.NotImplementedException();
        }

        public async Task<CompanyDto> GetCompanyData(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<UserDetailsDto>> GetListOfEmployees(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> CreateManager(CreateManagerDto data)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(data.UserId.ToString());
                var manager = new Manager
                {
                    User = user
                };

                await _context.Manager.AddAsync(manager);
                await _context.SaveChangesAsync();
                return manager.Id;


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<int> CreateBranchManager(CreateManagerDto data)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateBranch(CreateBranchDto data)
        {
            throw new NotImplementedException();
        }
    }
}