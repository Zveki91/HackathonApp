using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackathonApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HackathonApp.Helpers
{
    public class Seed
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public Seed(DataContext context, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _context = context;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }


        public async Task SeedDb()
        {
            #region Roles

            RoleManager<Role> roleManager = _serviceProvider.GetRequiredService<RoleManager<Role>>();
            UserManager<ApplicationUser> userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = {"Customer", "Employee", "Administrator"};
            IdentityResult roleResult;

            // Create the roles and seed them to the database, if they don't already exist
            foreach (string roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new Role(roleName));
                }
            }
            
            #endregion

            #region Users


            var password = "Password123!";
            var companyManager1 = new ApplicationUser()
            {
                FirstName = "Market",
                LastName = "Manager",
                UserName = "manager@company.com",
                Email = "manager@company.com",
                Wallet = WalletGenerator.Create()
            };
            var companyManager2 = new ApplicationUser()
            {
                FirstName = "Frizer",
                LastName = "Manager",
                UserName = "manager@company2.com",
                Email = "manager@company2.com",
                Wallet = WalletGenerator.Create()
            };
            var companyManager3 = new ApplicationUser()
            {
                FirstName = "Krojac",
                LastName = "Manager",
                UserName = "manager@company3.com",
                Email = "manager@company3.com",
                Wallet = WalletGenerator.Create()
            };
            var branchManager1 = new ApplicationUser()
            {
                FirstName = "Branch1",
                LastName = "Manager",
                UserName = "manager@branch1.com",
                Email = "manager@branch1.com",
                Wallet = WalletGenerator.Create()
            };
               var branchManager2 = new ApplicationUser()
              {
                  FirstName = "Branch2",
                  LastName = "Manager",
                  UserName = "manager@branch2.com",
                  Email = "manager@branch2.com",
                  Wallet = WalletGenerator.Create()
              };
               var branchManager3 = new ApplicationUser()
               {
                   FirstName = "Branch3",
                   LastName = "Manager",
                   UserName = "manager@branch3.com",
                   Email = "manager@branch3.com",
                   Wallet = WalletGenerator.Create()
               };
               var customersList = new List<ApplicationUser>()
               {
                   new ApplicationUser()
                   {
                       FirstName = "customer",
                       LastName = "cust 1",
                       UserName = "customer@cust1.com",
                       Email = "customer@cust1.com",
                       Wallet = WalletGenerator.Create()
                   },
                   new ApplicationUser()
                   {
                       FirstName = "customer",
                       LastName = "cust 2",
                       UserName = "customer@cust2.com",
                       Email = "customer@cust2.com",
                       Wallet = WalletGenerator.Create()
                   },
                   new ApplicationUser()
                   {
                       FirstName = "customer",
                       LastName = "cust 3",
                       UserName = "customer@cust3.com",
                       Email = "customer@cust3.com",
                       Wallet = WalletGenerator.Create()
                   },
                   new ApplicationUser()
                   {
                       FirstName = "customer",
                       LastName = "cust 4",
                       UserName = "customer@cust4.com",
                       Email = "customer@cust4.com",
                       Wallet = WalletGenerator.Create()
                   },
                   new ApplicationUser()
                   {
                       FirstName = "customer",
                       LastName = "cust 5",
                       UserName = "customer@cust5.com",
                       Email = "customer@cust5.com",
                       Wallet = WalletGenerator.Create()
                   }
               };


                var createCompanyManager1 = await userManager.CreateAsync(companyManager1, password);
            
                if (createCompanyManager1.Succeeded)
                {
                    await userManager.AddToRoleAsync(companyManager1, "Employee");
                    await _context.SaveChangesAsync();
                }
                
                var createCompanyManager2 = await userManager.CreateAsync(companyManager2, password);
            
                if (createCompanyManager2.Succeeded)
                {
                    await userManager.AddToRoleAsync(companyManager2, "Employee");
                    await _context.SaveChangesAsync();
                }
                
                var createCompanyManager3 = await userManager.CreateAsync(companyManager3, password);
            
                if (createCompanyManager3.Succeeded)
                {
                    await userManager.AddToRoleAsync(companyManager3, "Employee");
                    await _context.SaveChangesAsync();
                }
                
               var createBranchManager1 =  await userManager.CreateAsync(branchManager1, password);
               if (createBranchManager1.Succeeded)
               {
                   await userManager.AddToRoleAsync(branchManager1, "Employee");
                   await _context.SaveChangesAsync();
               }
               var createBranchManager2 = await userManager.CreateAsync(branchManager2, password);
               if (createBranchManager2.Succeeded)
               {
                   await userManager.AddToRoleAsync(branchManager2, "Employee");
                   await _context.SaveChangesAsync();
               }
               var createBranchManger3 =  await userManager.CreateAsync(branchManager3, password);
               if (createBranchManger3.Succeeded)
               {
                   await userManager.AddToRoleAsync(branchManager3, "Employee");
                   await _context.SaveChangesAsync();
               }
               for (int i = 0; i < customersList.Count; i++)
               {
                   var createCustomer = await userManager.CreateAsync(customersList[i], password);
                   if (createCustomer.Succeeded)
                   {
                       await userManager.AddToRoleAsync(customersList[i], "Customer");
                       await _context.SaveChangesAsync();
                   }
               }
               



               #endregion

            #region Managers and branch managers

               var newManagerList = new List<Manager>()
               {
                   new Manager
                   {
                       Id = Guid.NewGuid(),
                       User = companyManager1
                   },
                   new Manager
                   {
                       Id = Guid.NewGuid(),
                       User = companyManager2
                   },
                   new Manager
                   {
                       Id = Guid.NewGuid(),
                       User = companyManager3
                   },
               };

               var bManagersList = new List<BranchManager>()
               {
                   new BranchManager
                   {
                       Id = Guid.NewGuid(),
                       User = branchManager1
                   },
                   new BranchManager
                   {
                       Id = Guid.NewGuid(),
                       User = branchManager2
                   },
                   new BranchManager
                   {
                       Id = Guid.NewGuid(),
                       User = branchManager3
                   },
                   new BranchManager
                   {
                       Id = Guid.NewGuid(),
                       User = companyManager2
                   },
                   new BranchManager
                   {
                       Id = Guid.NewGuid(),
                       User = companyManager2
                   }
               };

               #endregion



               #region Categories and Articles

               var categoryList = new List<Category>()
               {
                   new Category
                   {
                       Id = Guid.NewGuid(),
                       Name = "Usluga"
                   },
                   new Category
                   {
                       Id = Guid.NewGuid(),
                       Name = "Prehana"
                   },
                   new Category
                   {
                       Id = Guid.NewGuid(),
                       Name = "Ljubimci"
                   },
                   new Category
                   {
                       Id = Guid.NewGuid(),
                       Name = "Alat"
                   },
                   new Category
                   {
                       Id = Guid.NewGuid(),
                       Name = "Knjige"
                   },
                    new Category
                   {
                       Id = Guid.NewGuid(),
                       Name = "Elektronika"
                   },
                    new Category
                   {
                       Id = Guid.NewGuid(),
                       Name = "Bijela tehnika"
                   }
               };


               var articleList = new List<Article>()
               {
                   new Article
                   {
                       Id = Guid.NewGuid(),
                       Category = categoryList[0],
                       Name = "Sisanje",
                       Price = 5,
                       ImageUrl = null,
                       Domestic = true
                   },
                   new Article
                   {
                       Id = Guid.NewGuid(),
                       Category = categoryList[0],
                       Name = "Feniranje",
                       Price = 5,
                       ImageUrl = null,
                       Domestic = true
                   },
                   new Article
                   {
                       Id = Guid.NewGuid(),
                       Category = categoryList[0],
                       Name = "Brijanje",
                       Price = 5,
                       ImageUrl = null,
                       Domestic = true
                   },
                   new Article
                   {
                       Id = Guid.NewGuid(),
                       Category = categoryList[0],
                       Name = "Farbanje",
                       Price = 10,
                       ImageUrl = null,
                       Domestic = true
                   },
                   new Article
                   {
                       Id = Guid.NewGuid(),
                       Category = categoryList[0],
                       Name = "Pranje kose",
                       Price = 5,
                       ImageUrl = null,
                       Domestic = true
                   },
                   new Article
                   {
                       Id = Guid.NewGuid(),
                       Category = categoryList[0],
                       Name = "Usluge sivenja",
                       Price = 5,
                       ImageUrl = null,
                       Domestic = true
                   },
                   new Article
                   {
                       Id = Guid.NewGuid(),
                       Category = categoryList[0],
                       Name = "Prodaja odjece",
                       Price = 10,
                       ImageUrl = null,
                       Domestic = true
                   },
                   new Article
                   {
                       Id = Guid.NewGuid(),
                       Category = categoryList[1],
                       Name = "Chips Marbo",
                       Price = 1,
                       ImageUrl = null,
                       Domestic = true
                   },
                   new Article
                   {
                       Id = Guid.NewGuid(),
                       Category = categoryList[1],
                       Name = "Coca-Cola Zero",
                       Price = 1,
                       ImageUrl = null,
                       Domestic = false
                   },
                   new Article
                   {
                       Id = Guid.NewGuid(),
                       Category = categoryList[1],
                       Name = "Brasno T-400 5kg",
                       Price = 5,
                       ImageUrl = null,
                       Domestic = true
                   },
                   new Article
                   {
                       Id = Guid.NewGuid(),
                       Category = categoryList[1],
                       Name = "Ulje Sunce 1l",
                       Price = 3,
                       ImageUrl = null,
                       Domestic = true
                   },
                   new Article
                   {
                       Id = Guid.NewGuid(),
                       Category = categoryList[2],
                       Name = "Hrana za psa 12kg",
                       Price = 20,
                       ImageUrl = null,
                       Domestic = false
                   },
                   new Article
                   {
                       Id = Guid.NewGuid(),
                       Category = categoryList[2],
                       Name = "Hrana za macku 2kg",
                       Price = 8,
                       ImageUrl = null,
                       Domestic = false
                   },
                   new Article
                   {
                       Id = Guid.NewGuid(),
                       Category = categoryList[2],
                       Name = "Akvarijum 30x20",
                       Price = 30,
                       ImageUrl = null,
                       Domestic = false
                   },
                   new Article
                   {
                       Id = Guid.NewGuid(),
                       Category = categoryList[3],
                       Name = "Busilica Bosh",
                       Price = 55,
                       ImageUrl = null,
                       Domestic = false
                   },
                   new Article
                   {
                       Id = Guid.NewGuid(),
                       Category = categoryList[3],
                       Name = "Tacke Limex",
                       Price = 75,
                       ImageUrl = null,
                       Domestic = true
                   },
                   new Article
                   {
                       Id = Guid.NewGuid(),
                       Category = categoryList[3],
                       Name = "Set Alata",
                       Price = 40,
                       ImageUrl = null,
                       Domestic = false
                   },
                   new Article
                   {
                       Id = Guid.NewGuid(),
                       Category = categoryList[4],
                       Name = "Knjiga Java 1",
                       Price = 15,
                       ImageUrl = null,
                       Domestic = false
                   },
                   new Article
                   {
                       Id = Guid.NewGuid(),
                       Category = categoryList[4],
                       Name = "Knjiga Java 2",
                       Price = 25,
                       ImageUrl = null,
                       Domestic = false
                   },
                   new Article
                   {
                       Id = Guid.NewGuid(),
                       Category = categoryList[4],
                       Name = "Knjiga JS Vanila",
                       Price = 35,
                       ImageUrl = null,
                       Domestic = false
                   },
                   new Article
                   {
                       Id = Guid.NewGuid(),
                       Category = categoryList[4],
                       Name = "Pisite los PHP code",
                       Price = 15,
                       ImageUrl = null,
                       Domestic = false
                   },
                   new Article
                   {
                       Id = Guid.NewGuid(),
                       Category = categoryList[5],
                       Name = "Smart Phone 1",
                       Price = 105,
                       ImageUrl = null,
                       Domestic = false
                   },
                   new Article
                   {
                       Id = Guid.NewGuid(),
                       Category = categoryList[5],
                       Name = "Smart Phone 2",
                       Price = 125,
                       ImageUrl = null,
                       Domestic = false
                   },
                   new Article
                   {
                       Id = Guid.NewGuid(),
                       Category = categoryList[5],
                       Name = "Smart Phone 3",
                       Price = 205,
                       ImageUrl = null,
                       Domestic = false
                   },
                   new Article
                   {
                       Id = Guid.NewGuid(),
                       Category = categoryList[5],
                       Name = "Tablet 1",
                       Price = 165,
                       ImageUrl = null,
                       Domestic = false
                   },
                   new Article
                   {
                       Id = Guid.NewGuid(),
                       Category = categoryList[5],
                       Name = "Tablet 2",
                       Price = 185,
                       ImageUrl = null,
                       Domestic = false
                   },
                   new Article
                   {
                       Id = Guid.NewGuid(),
                       Category = categoryList[6],
                       Name = "Sporet Gorenje",
                       Price = 255,
                       ImageUrl = null,
                       Domestic = true
                   },
                   new Article
                   {
                       Id = Guid.NewGuid(),
                       Category = categoryList[6],
                       Name = "Frizider Gorenje",
                       Price = 255,
                       ImageUrl = null,
                       Domestic = true
                   },
                   new Article
                   {
                       Id = Guid.NewGuid(),
                       Category = categoryList[6],
                       Name = "Frizider Bosch",
                       Price = 255,
                       ImageUrl = null,
                       Domestic = false
                   }
               };
               
               #endregion
            

               #region Company and Branches


               var companyList = new List<Company>()
               {
                   new Company
                   {
                       Id = Guid.NewGuid(),
                       Name = "Market",
                       Address = "Banja Luka , Cara Lazara 23",
                       Branches = null,
                       Manager = newManagerList[0],
                       Employees = new List<ApplicationUser>()
                       {
                           branchManager1,
                           branchManager2,
                           branchManager3,
                           companyManager1
                       }
                   },
                   new Company
                   {
                       Id = Guid.NewGuid(),
                       Name = "Frizer",
                       Address = "Banja Luka , Cara Lazara 23",
                       Branches = null,
                       Manager = newManagerList[1],
                       Employees = new List<ApplicationUser>()
                       {
                           companyManager2
                       }
                   },
                   new Company
                   {
                       Id = Guid.NewGuid(),
                       Name = "Krojac",
                       Address = "Banja Luka , Cara Lazara 23",
                       Branches = null,
                       Manager = newManagerList[2],
                       Employees = new List<ApplicationUser>()
                       {
                           companyManager3
                       }
                   }

               };

               var branchList = new List<Branch>()
               {
                   new Branch
                   {
                       Id = Guid.NewGuid(),
                       Name = "Poslovna jedinica Starcevica",
                       Location = "Banja Luka",
                       Company = companyList[0],
                       Discounts = null,
                       BranchManager = bManagersList[0]
                   },
                   new Branch
                   {
                       Id = Guid.NewGuid(),
                       Name = "Poslovna jedinica Mejdan",
                       Location = "Banja Luka",
                       Company = companyList[0],
                       Discounts = null,
                       Articles = null,
                       BranchManager = bManagersList[1]
                   },
                   new Branch
                   {
                       Id = Guid.NewGuid(),
                       Name = "Poslovna jedinica Borik",
                       Location = "Banja Luka",
                       Company = companyList[0],
                       Discounts = null,
                       Articles = null,
                       BranchManager = bManagersList[2]
                   },
                   new Branch
                   {
                       Id = Guid.NewGuid(),
                       Name = "Poslovna jedinica Borik",
                       Location = "Banja Luka",
                       Company = companyList[1],
                       Discounts = null,
                       Articles = null,
                       BranchManager = bManagersList[3]
                   },
                   new Branch
                   {
                       Id = Guid.NewGuid(),
                       Name = "Poslovna jedinica Borik",
                       Location = "Banja Luka",
                       Company = companyList[2],
                       Discounts = null,
                       Articles = null,
                       BranchManager = bManagersList[4]
                   },
                   
               };
               
               var marketCategoriesList =  categoryList.Where(x => x.Id != categoryList[0].Id).Select(x => x.Id).ToList();
               var marketBranchArticles = articleList.Where(x => marketCategoriesList.Contains(x.Category.Id)).ToList();

               var fArticles = articleList.Where(x =>
                       x.Category.Id == categoryList[0].Id && x.Name != "Usluge sivenja" || x.Name != "Prodaja odjece")
                   .ToList();
               
               var kArticles =  articleList.Where(x =>
                       x.Category.Id == categoryList[0].Id && x.Name == "Usluge sivenja" || x.Name == "Prodaja odjece")
                   .ToList();
               
               branchList[0].Articles = marketBranchArticles.Select(x => new BranchArticles
               {
                   Id = Guid.NewGuid(),
                   Branch = branchList[0],
                   Article = x
               }).ToList();
               
               branchList[1].Articles = marketBranchArticles.Select(x => new BranchArticles
               {
                   Id = Guid.NewGuid(),
                   Branch = branchList[1],
                   Article = x
               }).ToList();
               
               branchList[2].Articles = marketBranchArticles.Select(x => new BranchArticles
               {
                   Id = Guid.NewGuid(),
                   Branch = branchList[2],
                   Article = x
               }).ToList();
               
               branchList[3].Articles = marketBranchArticles.Select(x => new BranchArticles
               {
                   Id = Guid.NewGuid(),
                   Branch = branchList[3],
                   Article = x
               }).ToList();
               
               
               branchList[4].Articles = marketBranchArticles.Select(x => new BranchArticles
               {
                   Id = Guid.NewGuid(),
                   Branch = branchList[4],
                   Article = x
               }).ToList();
               
               #endregion



               await _context.Category.AddRangeAsync(categoryList);
               await _context.Article.AddRangeAsync(articleList);
               await _context.Company.AddRangeAsync(companyList);
               await _context.Manager.AddRangeAsync(newManagerList);
               await _context.Branch.AddRangeAsync(branchList);
               await _context.BranchManager.AddRangeAsync(bManagersList);

               await _context.SaveChangesAsync();
        }
    }
}