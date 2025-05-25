
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using common.Dto;

namespace DL.Interfaces
{
    public interface IGiftRepository
    {
        Task<List<GiftDto>> Get();
        Task<Gift> Get(int id);
        Task Add(Gift gift);
        Task Update(int id, GiftDto gift);
        Task<bool> Delete(int id);
        Task<List<GiftDto>> Search(string giftName = null, string donorName = null, int? buyerCount = null);
        Task<DonorDto> GetDonor(int giftId);
        public Task<bool> TitleExists(string title);
        public Task<List<GiftDto>> SortByPrice();
        public Task<List<GiftDto>> SortByCategory();
        public Task UpdateWinnerId(int id, int winnerId);
    }
}
