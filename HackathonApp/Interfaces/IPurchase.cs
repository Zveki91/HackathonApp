using System;
using System.Threading.Tasks;
using HackathonApp.Dto;
using HackathonApp.Dto.Purchases;
using HackathonApp.Helpers;

namespace HackathonApp.Interfaces
{
    public interface IPurchase
    {
        Task<PurchaseDto> CreatePurchase(CreatePurchaseDto data);

        Task<PurchaseDto> CreateRedeemPurchase(CreateRedeemPurchaseDto data);

        Task<PurchaseDto> GetPurchase(Guid id, Guid userId);

        Task<PaginatedList<PurchaseDto>> GetListOfPurchasesForCompany(Guid companyId);

        Task<PaginatedList<PurchaseDto>> GetListOfPurchasesForUser(Guid userId);
    }
}