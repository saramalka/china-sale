using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL.Repositories;
using AutoMapper;
using BLL.Dto;
using DL.Entities;

namespace BLL.Services
{
    public class PurchaseService
    {
        private readonly IPurchaseRepository purchaseRrpository;
        private readonly IMapper mapper;

        public PurchaseService(IPurchaseRepository purchaseRrpository, IMapper mapper)
        {
            this.purchaseRrpository = purchaseRrpository;
            this.mapper = mapper;       
        }

        public async Task<IEnumerable<PurchaseDto>> GetAllAsync()
        {
            var purchases=await purchaseRrpository.GetAllAsync();
            return mapper.Map<IEnumerable<PurchaseDto>>(purchases);
        }

        public async Task<PurchaseDto?> GetByIdAsync(int id)
        {
           var purchase=await purchaseRrpository.GetByIdAsync(id);
            return mapper.Map<PurchaseDto>(purchase);

        }

        public async Task<PurchaseDto> CreateAsync(PurchaseDto dto)
        {
           var purchase=mapper.Map<Purchase>(dto);

            var created = await purchaseRrpository.AddAsync(purchase);
            return mapper.Map<PurchaseDto>(created);

        }

        public async Task UpdateAsync(int id, PurchaseDto dto)
        {
            var purchase = await purchaseRrpository.GetByIdAsync(id);
            if (purchase == null) return;

            purchase.Quantity = dto.Quantity;
            purchase.GiftId=dto.GiftId;
            purchase.UserId=dto.UserId;
            purchase.Date=dto.Date;
            
            await purchaseRrpository.UpdateAsync(purchase);
        }

        public async Task DeleteAsync(int id)
        {
            await purchaseRrpository.DeleteAsync(id);
        }
    }
}


