using AutoMapper;
using BLL.Dto;
using DL.Entities;
using DL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class DonationService
    {
        private readonly IDonationRepository donationRepository;
        private readonly IMapper mapper;

        public DonationService(IDonationRepository donationRepository, IMapper mapper)
        {
            this.donationRepository = donationRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<TicketDto>> GetAllAsync()
        {
            var donations =await donationRepository.GetAllAsync();
            return mapper.Map<IEnumerable<TicketDto>>(donations);
        }

        public async Task<TicketDto> GetByIDAsync(int id)
        {
            var donation =await donationRepository.GetByIdAsync(id);
            return mapper.Map<TicketDto>(donation);
        }

        public async Task<TicketDto> CreateAsync(TicketDto donationDto)
        {
            var donation = mapper.Map<Donation>(donationDto);
            var created = await donationRepository.AddAsync(donation);
            return mapper.Map<TicketDto>(created);
        }
        public async Task UpdateAsync(int id, TicketDto donationDto)
        {
            var donation = await donationRepository.GetByIdAsync(id);
            if (donation == null) return;

            donation.DonorId=donationDto.DonorId;
            donation.Id=donationDto.Id;
            donation.Amount =donationDto.Amount;
        
          
            await donationRepository.UpdateAsync(donation);

        }

        public async Task DeleteAsync(int id)
        {
            await donationRepository.DeleteAsync(id);
        }
    }

}
