using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HackathonApp.Dto;

namespace HackathonApp.Interfaces
{
    public interface ICompanies
    {
        Task<Guid> CreateCompany(CreateCompanyDto data);

        Task<CompanyDto> GetCompanyData(Guid id);

        Task<List<UserDetailsDto>> GetListOfEmployees(Guid id);

        Task<Guid> CreateManager(CreateManagerDto data);

        Task<Guid> CreateBranchManager(CreateManagerDto data);

        Task<Guid> CreateBranch(CreateBranchDto data);


    }
}