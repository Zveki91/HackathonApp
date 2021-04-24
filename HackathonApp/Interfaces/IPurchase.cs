using System;
using System.Threading.Tasks;
using HackathonApp.Dto;
using HackathonApp.Helpers;

namespace HackathonApp.Interfaces
{
    public interface IPurchase
    {
        Task<Guid> CreatePurchase(CreatePurchaseDto data);

        Task<PurchaseDto> GetPurchase(Guid id);

        Task<PaginatedList<PurchaseDto>> GetListOfPurchasesForCompany(Guid companyId);

        Task<PaginatedList<PurchaseDto>> GetLifOfPurchasesForUser(Guid userId);
    }
}