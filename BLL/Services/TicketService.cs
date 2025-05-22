using BLL.Interfaces;
using common.Dto;
using DL.Entities;
using DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TicketService:ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IGiftRepository _giftRepository;
        public TicketService(ITicketRepository ticketRepository,IGiftRepository giftRepository)
        {
               _ticketRepository = ticketRepository; 
               _giftRepository = giftRepository;
        }

        public async Task<IEnumerable<TicketDto>> Get()
        {
            var ticket=await _ticketRepository.Get(); 
            return ticket;

        }

        public async Task<IEnumerable<TicketDto>> GetByUserPaid()
        {
            return await _ticketRepository.GetByUserPaid();
        }

        public async Task<IEnumerable<TicketDto>> GetByUserPending()
        {
            return await _ticketRepository.GetByUserPending();
        }

        public async Task<TicketDto> Get(int id)
        {
            return await _ticketRepository.Get(id);
        }



        public async Task Add(Ticket ticket)
        {
            await _ticketRepository.Add(ticket);
        }

        public async Task pay(int id)
        {
            await _ticketRepository.pay(id);
        }

        public async Task Delete(int id)
        {
            await _ticketRepository.Delete(id);
        }
    }
}
