using AutoMapper;
using common.Dto;
using DL.Entities;
using DL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL.Interfaces;


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
        public async Task<IEnumerable<DonorDto>> Get()
        {
            return await donorRepository.Get();
        }
        public async Task<DonorDto> Get(int id)
        {
            return await donorRepository.Get(id);
        }
        public async Task Add(Donor donor)
        {
            await donorRepository.Add(donor);
        }
        public async Task Update(int id, DonorDto donorDto)
        {
            await donorRepository.Update(id, donorDto);
        }
        public async Task Delete(int id)
        {
            await donorRepository.Delete(id);
        }
        public async Task<IEnumerable<DonorDto>> Search(string name = null, string email = null, string giftName = null)
        {
            return await donorRepository.Search(name, email, giftName);
        }
    }
}
