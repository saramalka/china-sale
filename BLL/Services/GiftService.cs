using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Dto;
using DL.Entities;
using DL.Interfaces;

namespace BLL.Services
{
    public class GiftService
    {
        private readonly IGiftRepository giftRepository;
        private readonly IMapper mapper;

        public GiftService(IGiftRepository giftRepository, IMapper mapper)
        {
            this.giftRepository = giftRepository;
            this.mapper = mapper;
        }

        public  async Task<IEnumerable<GiftDto>> GetAllAsync()
        {
            var gifts=await giftRepository.GetAllAsync();
            return mapper.Map<IEnumerable<GiftDto>>(gifts);
        }

        public async Task<GiftDto> GetByIDAsync(int id)
        {
            var gift =await giftRepository.GetByIdAsync(id);             
            return mapper.Map<GiftDto>(gift);
        }

        public async Task<GiftDto> CreateAsync(GiftDto giftDto)
        {
            var gift=mapper.Map<Gift>(giftDto);
            var created=await giftRepository.AddAsync(gift);
            return mapper.Map<GiftDto>(created);
        }
        public async Task UpdateAsync(int id,GiftDto giftDto)
        {
            var gift=await giftRepository.GetByIdAsync(id);
            if(gift==null) return;

            gift.Id= giftDto.Id;
       
            gift.Name = giftDto.Name;
            gift.Category = giftDto.Category;
            gift.Description = giftDto.Description;
            gift.PricePerTicket= giftDto.PricePerTicket;
            gift.DonationId= giftDto.DonationId;
            gift.WinnerId= giftDto.WinnerId;

            await giftRepository.UpdateAsync(gift);
        }

      
        public async Task DeleteAsync(int id)
        {
            await giftRepository.DeleteAsync(id);
        }
    }
}
