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
    public class DonorService
    {
        private readonly IDonorRepository donorRepository;
        private readonly IMapper mapper;

        public DonorService(IDonorRepository donorRepository, IMapper mapper)
        {
            this.donorRepository = donorRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<DonorDto>> GetAllAsync()
        {
            var donors = donorRepository.GetAllAsync();
            return mapper.Map<IEnumerable<DonorDto>>(donors);
        }

        public async Task<DonorDto> GetByIDAsync(int id)
        {
            var donor = donorRepository.GetByIdAsync(id);
            return mapper.Map<DonorDto>(donor);
        }

        public async Task<DonorDto> CreateAsync(DonorDto donorDto)
        {
            var donor = mapper.Map<Donor>(donorDto);
            var created = await donorRepository.AddAsync(donor);
            return mapper.Map<DonorDto>(created);
        }
        public async Task UpdateAsync(int id, DonorDto donorDto)
        {
            var donor = await donorRepository.GetByIdAsync(id);
            if (donor == null) return;

            donor.Email = donorDto.Email;
            donor.Id = donorDto.Id;
            donor.Phone = donorDto.Phone;
            donor.Donations = donorDto.Donations;
            donor.FullName = donorDto.FullName;

            await donorRepository.UpdateAsync(donor);
        }

        public async Task DeleteAsync(int id)
        {
            await donorRepository.DeleteAsync(id);
        }
    }
}
