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

        public GiftService(IGiftRepository giftRepository, ITicketRepository ticketRepository)
        {
            this.giftRepository = giftRepository;
            this.ticketRepository = ticketRepository;
        }

        public async Task<IEnumerable<GiftDto>> Get()
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
            if (gift == null || gift.CategoryId == 0 || gift.DonorId == 0 || gift.GiftName == null)
            {
               throw new InvalidOperationException("Gift data cannot be null.");
            }
            if (gift.Price < 10 || gift.Price > 100)
            {
                throw new InvalidOperationException("Price must be between 10 and 100.");
            }

            var existingGift = await Get(id);
            if (existingGift == null)
            {
                throw new KeyNotFoundException($"gift with id {id} not found.");
            }

            var existingGiftWithSameName = await TitleExists(gift.GiftName);
            if (existingGiftWithSameName && existingGift.GiftName != gift.GiftName)
            {
                throw new DuplicateWaitObjectException("Gift with this name already exists.");
            }
            await giftRepository.Update(id, gift);
            
        }
        public async Task<bool> Delete(int id)
        {
            var existingGift = await Get(id);
            if (existingGift == null)
            {
                throw new KeyNotFoundException($"gift with id {id} not found.");
            }
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
