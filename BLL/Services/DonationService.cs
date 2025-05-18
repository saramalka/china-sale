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

        public async Task<IEnumerable<DonationDto>> GetAllAsync()
        {
            var donations = donationRepository.GetAllAsync();
            return mapper.Map<IEnumerable<DonationDto>>(donations);
        }

        public async Task<DonationDto> GetByIDAsync(int id)
        {
            var donation = donationRepository.GetByIdAsync(id);
            return mapper.Map<DonationDto>(donation);
        }

        public async Task<DonationDto> CreateAsync(DonationDto donationDto)
        {
            var donation = mapper.Map<Donation>(donationDto);
            var created = await donationRepository.AddAsync(donation);
            return mapper.Map<DonationDto>(created);
        }
        public async Task UpdateAsync(int id, DonationDto donationDto)
        {
            var donation = await donationRepository.GetByIdAsync(id);
            if (donation == null) return;

            donation.Donor=donationDto.Donor;
            donation.DonorId=donationDto.DonorId;
            donation.Id=donationDto.Id;
            donation.Amount =donationDto.Amount;
            donation.Gifts =donationDto.Gifts;
          
            await donationRepository.UpdateAsync(donation);

        }

        public async Task DeleteAsync(int id)
        public async Task DeleteAsync(int id)
        {
            await giftRepository.DeleteAsync(id);
        }
    }
}
}
