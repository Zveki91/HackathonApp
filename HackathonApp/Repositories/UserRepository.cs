using System;
using System.Threading.Tasks;
using HackathonApp.Dto;
using HackathonApp.Helpers;
using HackathonApp.Interfaces;

namespace HackathonApp.Repositories
{
    public class UserRepository : IUsers
    {
        public async Task<PaginatedList<UserDetailsDto>> GetListOfUsers(int size, int page)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginatedList<UserDetailsDto>> GetListOfUsersForCompany(int companyId, int size, int page)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Login(LoginDto data)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateUser(Guid id, UserUpdateDto data)
        {
            throw new NotImplementedException();
        }
    }
}