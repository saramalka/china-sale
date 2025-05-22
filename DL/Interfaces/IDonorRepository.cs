using common.Dto;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Interfaces
{
    public interface IDonorRepository
    {
        public Task<List<DonorDto>> Get();
        public Task<DonorDto> Get(int id);
        public Task Add(Donor donor);
        public Task Update(int id, DonorDto donorDto);
        public Task Delete(int id);
        public Task<List<DonorDto>> Search(string name = null, string email = null, string giftName = null);
    }
}
