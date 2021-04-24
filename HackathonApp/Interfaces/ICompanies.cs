using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HackathonApp.Dto;

namespace HackathonApp.Interfaces
{
    public interface ICompanies
    {
        Task<int> CreateCompany(CreateCompanyDto data);

        Task<CompanyDto> GetCompanyData(int id);

        Task<List<UserDetailsDto>> GetListOfEmployees(int id);

        Task<int> CreateManager(CreateManagerDto data);

        Task<int> CreateBranchManager(CreateManagerDto data);

        Task<int> CreateBranch(CreateBranchDto data);


    }
}