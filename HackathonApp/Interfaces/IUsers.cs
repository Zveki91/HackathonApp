using System;
using System.Threading.Tasks;
using HackathonApp.Dto;
using HackathonApp.Helpers;

namespace HackathonApp.Interfaces
{
    public interface IUsers
    {
        public Task<Guid> CreteUser(CreateUserDto data);
        
        public Task<PaginatedList<UserDetailsDto>> GetListOfUsers(int size, int page);
        
        public Task<PaginatedList<UserDetailsDto>> GetListOfUsersForCompany(int companyId,int size, int page);
        public Task<LoginTokenDto> Login(LoginDto data);
        
        public Task<bool> UpdateUser(Guid id, UserUpdateDto data);
    }
}