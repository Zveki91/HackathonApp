using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackathonApp.Data;
using HackathonApp.Dto;
using HackathonApp.Dto.Exceptions;
using HackathonApp.Dto.Purchases;
using HackathonApp.Helpers;
using HackathonApp.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HackathonApp.Repositories
{
    public class PurchaseRepository : IPurchase
    {
        private readonly DataContext _context;

        private readonly IContract _contracts;

        private readonly UserManager<ApplicationUser> _users;

        public PurchaseRepository(DataContext context, IContract contracts, UserManager<ApplicationUser> users)
        {
            _users = users;
            _context = context;
            _contracts = contracts;
        }

        public async Task<PurchaseDto> CreatePurchase(CreatePurchaseDto data)
        {
            var branch = await _context.Branch.FirstOrDefaultAsync(x => x.Id == data.BranchId);
            if (branch == null)
                throw new MyNotFoundException("Branch not found.", 404);
            var customer = await _context.Users.FirstOrDefaultAsync(x => x.Id == data.CustomerId);
            if (customer == null) 
                throw new MyNotFoundException("Customer not found.", 404);
            List<Article> articles = await _context.Article.Where(x => data.Articles.Select(x => x.ArticleId)
                .Contains(x.Id)).ToListAsync();
            if (articles.Count == 0) throw new MyBadRequestException("Purchase must have at least 1 article.", 400);

            var newPurchase = new Purchase
            {
                Id = Guid.NewGuid(),
                Branch = branch,
                Customer = customer,
                TotalPrice = articles.Sum(x => x.Price),
                DiscountedPrice = 0,
                TokenAmount = 0,
                Articles = new List<ArticlePurchase>(),
                Date = DateTime.UtcNow
            };
            foreach(var a in articles)
            {
                Article dbArticle = _context.Article.FirstOrDefault(x => x.Id == a.Id);
                PurchaseArticleDto payloadArticle = data.Articles.FirstOrDefault(x => x.ArticleId == a.Id);
                decimal price = dbArticle.Price * payloadArticle.Quantity;

                Discount discount = _context.Discount.Include(x => x.Article)
                    .FirstOrDefault(x => x.Article.Id == a.Id);
                if (discount != null)
                    price = price - price * discount.PriceReduction / 100;
                newPurchase.Articles.Add(new ArticlePurchase()
                {
                    Quantity = payloadArticle.Quantity,
                    Article = dbArticle,
                    Id = Guid.NewGuid(),
                    Price = price,
                    Purchase = newPurchase
                });
            }
            newPurchase.DiscountedPrice = newPurchase.Articles.Sum(x => x.Price);
            int tokenAmount = await CalculateTokenReward.GetRewardAmount(newPurchase);
            newPurchase.TokenAmount = tokenAmount;

            await _context.Purchase.AddAsync(newPurchase);
            await _context.SaveChangesAsync();

            var user = await _users.FindByIdAsync(data.CustomerId.ToString());
            await _contracts.MintToken(user.Wallet, tokenAmount);
            return new PurchaseDto
            {
                Id = newPurchase.Id,
                BranchId = newPurchase.Branch.Id,
                UserId = newPurchase.Customer.Id,
                Articles = newPurchase.Articles.Select(x => new ArticleDto
                {
                    Id = x.Article.Id,
                    Name = x.Article.Name,
                    Price = x.Article.Price,
                }).ToList(),
                TotalPrice = newPurchase.TotalPrice,
                DiscountedPrice = newPurchase.DiscountedPrice,
                TokenAmount = tokenAmount
            };
        }

        public async Task<PurchaseDto> GetPurchase(Guid id, Guid userId)
        {
            var purchase = await _context.Purchase
                .Include(x => x.Branch)
                .ThenInclude(x => x.Company)
                .Include(x => x.Articles)
                .ThenInclude(x => x.Article)
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
                TotalPrice = purchase.TotalPrice,
                DiscountedPrice = purchase.DiscountedPrice,
                TokenAmount = purchase.TokenAmount
            };
        }

        public async Task<PaginatedList<PurchaseDto>> GetListOfPurchasesForCompany(Guid companyId)
        {
            var companyPurchases = await _context.Purchase
                .Include(x => x.Branch)
                .ThenInclude(x => x.Company)
                .Include(x => x.Articles)
                .ThenInclude(x => x.Article)
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
                TotalPrice = x.TotalPrice,
                DiscountedPrice = x.DiscountedPrice,
                TokenAmount = x.TokenAmount
            }).ToList();

            return new PaginatedList<PurchaseDto>(results.AsQueryable(), results.Count);
        }

        public async Task<PaginatedList<PurchaseDto>> GetListOfPurchasesForUser(Guid userId)
        {
            var purchaseList = await _context.Purchase
                .Include(x => x.Branch)
                .ThenInclude(x => x.Company)
                .Include(x => x.Articles)
                .ThenInclude(x => x.Article)
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
                TotalPrice = x.TotalPrice,
                DiscountedPrice = x.DiscountedPrice,
                TokenAmount = x.TokenAmount
            }).ToList();

            return new PaginatedList<PurchaseDto>(results.AsQueryable(), results.Count);
        }

        public async Task<PurchaseDto> CreateRedeemPurchase(CreateRedeemPurchaseDto data)
        {
            var branch = await _context.Branch.FirstOrDefaultAsync(x => x.Id == data.BranchId);
            if (branch == null)
                throw new MyNotFoundException("Branch not found.", 404);
            var customer = await _context.Users.FirstOrDefaultAsync(x => x.Id == data.CustomerId);
            if (customer == null)
                throw new MyNotFoundException("Customer not found.", 404);
            List<Article> articles = await _context.Article.Where(x => data.Articles.Select(x => x.ArticleId)
                .Contains(x.Id)).ToListAsync();
            if (articles.Count == 0) throw new MyBadRequestException("Purchase must have at least 1 article.", 400);
            
            var newPurchase = new Purchase
            {
                Id = Guid.NewGuid(),
                Branch = branch,
                Customer = customer,
                TotalPrice = articles.Sum(x => x.Price),
                DiscountedPrice = 0,
                TokenAmount = data.TokenAmount,
                Articles = new List<ArticlePurchase>(),
                Date = DateTime.UtcNow
            };
            foreach (var a in articles)
            {
                Article dbArticle = _context.Article.FirstOrDefault(x => x.Id == a.Id);
                PurchaseArticleDto payloadArticle = data.Articles.FirstOrDefault(x => x.ArticleId == a.Id);
                decimal price = dbArticle.Price * payloadArticle.Quantity;
                
                Discount discount = _context.Discount.Include(x => x.Article)
                    .FirstOrDefault(x => x.Article.Id == a.Id);
                if (discount != null)
                    price = price - price * discount.PriceReduction / 100;
                newPurchase.Articles.Add(new ArticlePurchase()
                {
                    Quantity = payloadArticle.Quantity,
                    Article = dbArticle,
                    Id = Guid.NewGuid(),
                    Price = price,
                    Purchase = newPurchase
                });
            }
            decimal discountTokenModifier = 
                newPurchase.Articles.Sum(x => x.Price) / (decimal)(data.TokenAmount * 0.04);

            foreach (var a in newPurchase.Articles)
                a.Price = a.Price - (a.Price / discountTokenModifier);
            
            newPurchase.DiscountedPrice = newPurchase.Articles.Sum(x => x.Price);

            newPurchase.TokenAmount = (-1) * newPurchase.TokenAmount;
            await _context.Purchase.AddAsync(newPurchase);
            await _context.SaveChangesAsync();
            newPurchase.TokenAmount = (-1) * newPurchase.TokenAmount;
            var user = await _users.FindByIdAsync(data.CustomerId.ToString());
            await _contracts.BurnToken(user.Wallet, newPurchase.TokenAmount);
            return new PurchaseDto
            {
                Id = newPurchase.Id,
                BranchId = newPurchase.Branch.Id,
                UserId = newPurchase.Customer.Id,
                Articles = newPurchase.Articles.Select(x => new ArticleDto
                {
                    Id = x.Article.Id,
                    Name = x.Article.Name,
                    Price = x.Article.Price,
                }).ToList(),
                TotalPrice = newPurchase.TotalPrice,
                DiscountedPrice = newPurchase.DiscountedPrice,
                TokenAmount = newPurchase.TokenAmount
            };
        }
    }
}