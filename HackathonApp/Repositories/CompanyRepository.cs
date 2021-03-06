using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackathonApp.Data;
using HackathonApp.Dto;
using HackathonApp.Dto.Exceptions;
using HackathonApp.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

        /// <summary>
        /// Create new Company
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<Guid> CreateCompany(CreateCompanyDto data)
        {
            var manager =await _context.Manager.FirstOrDefaultAsync(x => x.Id == data.ManagerId);
            
            var newCompany = new Company
            {
                Id = Guid.NewGuid(),
                Name = data.Name,
                Address = data.Address,
                Branches = null,
                Manager = manager,
                Employees = null
            };

            var result = await _context.Company.AddAsync(newCompany);
            await _context.SaveChangesAsync();
            return newCompany.Id;

        }

        /// <summary>
        /// Get Data for specified company
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="MyNotFoundException"></exception>
        public async Task<CompanyDto> GetCompanyData(Guid id)
        {
            var company = await _context.Company
                .Include(x => x.Manager)
                .ThenInclude(x => x.User)
                .Include(x => x.Branches)
                .ThenInclude(x => x.BranchManager)
                .ThenInclude(x => x.User)
                .Include(x => x.Employees)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (company == null) throw new MyNotFoundException("Company not found.", 404);
            var companyDetails = new CompanyDto
            {
                Id = company.Id,
                Name = company.Name,
                Employees = company.Employees.Select(x => new UserDetailsDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.UserName
                }).ToList(),
                ManagerName = $"{company.Manager.User.FirstName} {company.Manager.User.LastName}",
                Branches = company.Branches.Select(x => new BranchDetailsDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    ManagerName = $"{x.BranchManager.User.FirstName} {x.BranchManager.User.LastName}",
                    Address = x.Location
                }).ToList()
            };
            return companyDetails;
        }

        /// <summary>
        /// Get list of employees for company
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<UserDetailsDto>> GetListOfEmployees(Guid id)
        {
            var company = await _context.Company
                .Include(x => x.Employees)
                .FirstOrDefaultAsync( x => x.Id == id);
            if (company == null) throw new MyNotFoundException("Company not found.", 404);
            var result = company.Employees.Select(x => new UserDetailsDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.UserName,
                Company = company.Name
            }).ToList();
            return result;
        }
        
        
        /// <summary>
        /// Create new manager
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<Guid> CreateManager(CreateManagerDto data)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(data.UserId.ToString());
                var manager = new Manager
                {
                    Id = Guid.NewGuid(),
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

        /// <summary>
        /// Create new Branch Manager
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<Guid> CreateBranchManager(CreateManagerDto data)
        {
            var user = await _userManager.FindByIdAsync(data.UserId.ToString());
            var manager = new BranchManager()
            {
                Id = Guid.NewGuid(),
                User = user
            };

            await _context.BranchManager.AddAsync(manager);
            await _context.SaveChangesAsync();
            return manager.Id;
        }

        /// <summary>
        /// Create new Branch for company
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="MyNotFoundException"></exception>
        public async Task<Guid> CreateBranch(CreateBranchDto data)
        {
            var company = await _context.Company.FirstOrDefaultAsync(x => x.Id == data.CompanyId);
            if (company == null) throw new MyNotFoundException("Company not found.", 404);
            var branchManager = await _context.BranchManager.FirstOrDefaultAsync(x => x.Id == data.BranchManagerId);
            if (branchManager == null) throw new MyNotFoundException("Branch Manager not found.", 404);
            var newBranch = new Branch
            {
                Name = data.Name,
                Location = data.Location,
                Company = company,
                BranchManager = branchManager,
            };
            await _context.Branch.AddAsync(newBranch);
            await _context.SaveChangesAsync();
            return newBranch.Id;
        }

        public async Task<DiscountDto> CreateDiscount(CreateDiscountDto data)
        {
            var article = await _context.Article.FirstOrDefaultAsync(x => x.Id == data.ArticleId);
            if (article == null) throw new MyNotFoundException("Article not found.",404);
            var branch = await _context.Branch.FirstOrDefaultAsync(x => x.Id == data.Branch);
            if (branch == null) throw new MyNotFoundException("Branch not found.", 404);
            var newDiscount = new Discount
            {
                Id = Guid.NewGuid(),
                Name = data.Name,
                Article = article,
                Branch = branch,
                PriceReduction = data.PriceReduction
            };

            await _context.Discount.AddAsync(newDiscount);
            await _context.SaveChangesAsync();

            return new DiscountDto
            {
                Id = newDiscount.Id,
                Name = newDiscount.Name,
                ArticleName = article.Name,
                BranchName = branch.Name,
                PriceReduction = data.PriceReduction
            };
        }

        public async Task<DiscountDto> GetDiscount(Guid id)
        {
            var discount = await _context.Discount
                .Include(x => x.Article)
                .Include(x => x.Branch)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (discount == null) throw new MyNotFoundException("Discount not found.", 404);
            return new DiscountDto
            {
                Id = discount.Id,
                Name = discount.Name,
                ArticleName = discount.Article.Name,
                BranchName = discount.Branch.Name,
                PriceReduction = discount.PriceReduction
            };

        }

        public async Task<List<DiscountDto>> GetListOfDiscounts()
        {
            var discounts = await _context.Discount
                .Include(x => x.Article)
                .Include(x => x.Branch)
                .ToListAsync();
            if (discounts.Count == 0 ) throw new MyNotFoundException("No active Discounts.", 404);
            var result = discounts.Select(x => new DiscountDto
            {
                Id = x.Id,
                Name = x.Name,
                ArticleName = x.Article.Name,
                BranchName = x.Branch.Name,
                PriceReduction = x.PriceReduction
            }).ToList();
            return result;
        }
    }
}