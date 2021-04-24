using System;
using System.Linq;
using System.Threading.Tasks;
using HackathonApp.Data;
using HackathonApp.Dto;
using HackathonApp.Dto.Exceptions;
using HackathonApp.Helpers;
using HackathonApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HackathonApp.Repositories
{
    public class PurchaseRepository : IPurchase
    {
        private readonly DataContext _context;

        public PurchaseRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<PurchaseDto> CreatePurchase(CreatePurchaseDto data)
        {
            var branch = await _context.Branch.FirstOrDefaultAsync(x => x.Id == data.BranchId);
            if (branch == null) throw new MyNotFoundException("Branch not found.", 404);
            var customer = await _context.Users.FirstOrDefaultAsync(x => x.Id == data.CustomerId);
            if (customer == null) throw new MyNotFoundException("Customer not found.", 404);
            var articles = await _context.Article.Where(x => data.Articles.Contains(x.Id)).ToListAsync();
            if (articles.Count == 0) throw new MyBadRequestException("Purchase must have at least 1 article.", 400);

            var newPurchase = new Purchase
            {
                Id = Guid.NewGuid(),
                Branch = branch,
                Customer = customer,
                TotalPrice = articles.Sum(x => x.Price)
            };
            newPurchase.Articles = articles.Select(x => new ArticlePurchase
            {
                Id = Guid.NewGuid(),
                Article = x,
                Purchase = newPurchase
            }).ToList();

            await _context.Purchase.AddAsync(newPurchase);
            await _context.SaveChangesAsync();
            return new PurchaseDto
            {
                Id = newPurchase.Id,
                BranchId = newPurchase.Branch.Id,
                UserId = newPurchase.Customer.Id,
                Articles = newPurchase.Articles.Select(x => new ArticleDto
                {
                    Id = x.Article.Id,
                    Name = x.Article.Name,
                    Price = x.Article.Price
                }).ToList(),
                TotalPrice = newPurchase.TotalPrice
            };

        }

        public async Task<PurchaseDto> GetPurchase(Guid id, Guid userId)
        {
            var purchase = await _context.Purchase
                .Include(x => x.Branch)
                .ThenInclude(x => x.Company)
                .Include(x => x.Articles)
                .Include(x => x.Customer)
                .FirstOrDefaultAsync(x => x.Id == id && x.Customer.Id == userId);
            if (purchase == null) throw new MyNotFoundException("Purchase not found.", 404);
            return new PurchaseDto
            {
                Id = purchase.Id,
                BranchId = purchase.Branch.Id,
                UserId = purchase.Customer.Id,
                Articles = purchase.Articles.Select(x => new ArticleDto
                {
                    Id = x.Article.Id,
                    Name = x.Article.Name,
                    Price = x.Article.Price
                }).ToList(),
                TotalPrice = purchase.TotalPrice
            };
        }

        public async Task<PaginatedList<PurchaseDto>> GetListOfPurchasesForCompany(Guid companyId)
        {
            var companyPurchases = await _context.Purchase
                .Include(x => x.Branch)
                .ThenInclude(x => x.Company)
                .Include(x => x.Articles)
                .Include(x => x.Customer)
                .Where(x => x.Branch.Company.Id == companyId)
                .ToListAsync();
            if (companyPurchases.Count == 0) return new PaginatedList<PurchaseDto>(null, 0);

            var results = companyPurchases.Select(x => new PurchaseDto
            {
                Id = x.Id,
                BranchId = x.Branch.Id,
                UserId = x.Customer.Id,
                Articles = x.Articles.Select(x => new ArticleDto
                {
                    Id = x.Article.Id,
                    Name = x.Article.Name,
                    Price = x.Article.Price
                }).ToList(),
                TotalPrice = x.TotalPrice
            }).ToList();

            return new PaginatedList<PurchaseDto>(results.AsQueryable(), results.Count);
        }

        public async Task<PaginatedList<PurchaseDto>> GetLifOfPurchasesForUser(Guid userId)
        {
            var purchaseList = await _context.Purchase
                .Include(x => x.Branch)
                .ThenInclude(x => x.Company)
                .Include(x => x.Articles)
                .Include(x => x.Customer)
                .Where(x => x.Customer.Id == userId)
                .ToListAsync();
            if (purchaseList.Count == 0) throw new MyNotFoundException("Purchase not found.", 404);
            var results = purchaseList.Select(x => new PurchaseDto
            {
                Id = x.Id,
                BranchId = x.Branch.Id,
                UserId = x.Customer.Id,
                Articles = x.Articles.Select(x => new ArticleDto
                {
                    Id = x.Article.Id,
                    Name = x.Article.Name,
                    Price = x.Article.Price
                }).ToList(),
                TotalPrice = x.TotalPrice
            }).ToList();

            return new PaginatedList<PurchaseDto>(results.AsQueryable(), results.Count);
        }
    }
}