using System;
using System.Collections.Generic;
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
            };
            var companyManager2 = new ApplicationUser()
            {
                FirstName = "Frizer",
                LastName = "Manager",
                UserName = "manager@company2.com",
                Email = "manager@company2.com",
            };
            var companyManager3 = new ApplicationUser()
            {
                FirstName = "Krojac",
                LastName = "Manager",
                UserName = "manager@company3.com",
                Email = "manager@company3.com",
            };
            var branchManager1 = new ApplicationUser()
            {
                FirstName = "Branch1",
                LastName = "Manager",
                UserName = "manager@branch1.com",
                Email = "manager@branch1.com",
            };
               var branchManager2 = new ApplicationUser()
              {
                  FirstName = "Branch2",
                  LastName = "Manager",
                  UserName = "manager@branch2.com",
                  Email = "manager@branch2.com",
              };
               var branchManager3 = new ApplicationUser()
               {
                   FirstName = "Branch3",
                   LastName = "Manager",
                   UserName = "manager@branch3.com",
                   Email = "manager@branch3.com",
               };
               var customersList = new List<ApplicationUser>()
               {
                   new ApplicationUser()
                   {
                       FirstName = "customer",
                       LastName = "cust 1",
                       UserName = "customer@cust1.com",
                       Email = "customer@cust1.com",
                   },
                   new ApplicationUser()
                   {
                       FirstName = "customer",
                       LastName = "cust 2",
                       UserName = "customer@cust2.com",
                       Email = "customer@cust2.com",
                   },
                   new ApplicationUser()
                   {
                       FirstName = "customer",
                       LastName = "cust 3",
                       UserName = "customer@cust3.com",
                       Email = "customer@cust3.com",
                   },
                   new ApplicationUser()
                   {
                       FirstName = "customer",
                       LastName = "cust 4",
                       UserName = "customer@cust4.com",
                       Email = "customer@cust4.com",
                   },
                   new ApplicationUser()
                   {
                       FirstName = "customer",
                       LastName = "cust 5",
                       UserName = "customer@cust5.com",
                       Email = "customer@cust5.com",
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
                       Articles = null,
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
                   }
               };
               

               #endregion
               
              

               await _context.Company.AddRangeAsync(companyList);
               await _context.Manager.AddRangeAsync(newManagerList);
               await _context.Branch.AddRangeAsync(branchList);
               await _context.BranchManager.AddRangeAsync(bManagersList);

        }
    }
}