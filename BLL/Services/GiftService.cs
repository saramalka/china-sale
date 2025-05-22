using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using common.Dto;
using DL.Entities;
using DL.Interfaces;


namespace BLL.Services
{
    public class GiftService
    {
        private readonly IGiftRepository giftRepository;

        private readonly ITicketRepository ticketRepository;

        public GiftService(IGiftRepository giftRepository)
        {
            this.giftRepository = giftRepository;
        }

        public async Task<IEnumerable<Gift>> Get()
        {
            return await giftRepository.Get();
        }
        public async Task<Gift> Get(int id)
        {
            return await giftRepository.Get(id);
        }
        public async Task Add(Gift gift)
        {
            await giftRepository.Add(gift);
        }
        public async Task Update(int id, GiftDto gift)
        {
            await giftRepository.Update(id, gift);
        }
        public async Task<bool> Delete(int id)
        {
            return await giftRepository.Delete(id);
        }
        public async Task<IEnumerable<GiftDto>> Search(string giftName = null, string donorName = null, int? buyerCount = null)
        {
            return await giftRepository.Search(giftName, donorName, buyerCount);
        }
        public async Task<DonorDto> GetDonor(int giftId)
        {
            return await giftRepository.GetDonor(giftId);
        }
        public async Task<bool> TitleExists(string title)
        {
            return await giftRepository.TitleExists(title);
        }
        public async Task<IEnumerable<GiftDto>> SortByPrice()
        {
            return await giftRepository.SortByPrice();
        }
        public async Task<IEnumerable<GiftDto>> SortByCategory()
        {
            return await giftRepository.SortByCategory();
        }
        public async Task RaffleAsync(int giftId)
        {
           
            var gift = await giftRepository.Get(giftId);
            if (gift == null)
            {
                throw new InvalidOperationException("Gift not found");
            }

            if (gift.Winner != null)
            {
                throw new InvalidOperationException("Gift already has a winner");
            }

            var tickets = gift.Tickets;
            if (tickets == null || tickets.Count == 0)
            {
                throw new InvalidOperationException("No tickets sold for this gift");
            }

            var random = new Random();
            int winnerIndex = random.Next(0, tickets.Count);
            var winnerTicket = tickets[winnerIndex];

            
            await ticketRepository.Win(winnerTicket.Id);

            await giftRepository.UpdateWinnerId(giftId, winnerTicket.UserId);
        }

    }
}
