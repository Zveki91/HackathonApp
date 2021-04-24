using System;
using System.Threading.Tasks;
using HackathonApp.Data;
using HackathonApp.Dto;
using HackathonApp.Dto.Exceptions;
using HackathonApp.Helpers;
using HackathonApp.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HackathonApp.Repositories
{
    public class UserRepository : IUsers
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly TokenHelper _tokenHelper;
        private readonly DataContext _context;

        public UserRepository(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, TokenHelper tokenHelper, RoleManager<Role> roleManager, DataContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenHelper = tokenHelper;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<Guid> CreteUser(CreateUserDto data)
        {
            try
            {
                var check = await _userManager.FindByEmailAsync(data.Email);
                if(check != null) throw new MyBadRequestException("Email already in use.", 400);
                var user = new ApplicationUser()
                {
                    Email = data.Email,
                    Id = Guid.NewGuid(),
                    UserName = data.Email,
                    FirstName = data.FirstName,
                    LastName = data.LastName
                };

                if (data.CompanyName != null)
                {
                    var companyCheck = await _context.Company
                        .Include(x => x.Employees)
                        .FirstOrDefaultAsync(x => x.Name == data.CompanyName); 
                    if (companyCheck == null) throw new MyNotFoundException("Company not found.",404);
                    companyCheck.Employees.Add(user);
                    _context.Update(companyCheck);
                }
                var role = await _roleManager.FindByNameAsync(data.Role);
                if (role == null) throw new MyNotFoundException("Role not found.", 404);
                var create = await _userManager.CreateAsync(user, data.Password);
                var addRole = await _userManager.AddToRoleAsync(user, role.Name);
                await ClaimsHelper.AddClaims(_userManager, user);
                if (create.Succeeded) return user.Id;
                throw new MyBadRequestException("Error while creating new Employee.", 400);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<PaginatedList<UserDetailsDto>> GetListOfUsers(int size, int page)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginatedList<UserDetailsDto>> GetListOfUsersForCompany(int companyId, int size, int page)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginTokenDto> Login(LoginDto data)
        {
            try
            {
                var check = await _userManager.FindByEmailAsync(data.Email);
                if (check == null) throw new MyNotFoundException("User not found", 404);
                var result = await _signInManager.PasswordSignInAsync(check, data.Password, false, false);
                if (!result.Succeeded) throw new MyBadRequestException("Username or Password Incorrect.", 400);
                var appRole = await _userManager.GetRolesAsync(check);
                var token = (string)await _tokenHelper.GenerateJwtToken(check, appRole[0]);
                return new LoginTokenDto()
                {
                    Token = token,
                    TokenType = "Bearer "
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> UpdateUser(Guid id, UserUpdateDto data)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDetailsDto> GetUserDetails(Guid id, string role)
        {
            try
            {
                var currentUser = await CurrentUserHelper.GetUserDetails(_context,_userManager,id, role);
                return currentUser;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }
    }
}