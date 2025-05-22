using common.Dto;
using DL.Entities;
using DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        public Task Add(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TicketDto>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<TicketDto> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TicketDto>> GetByUserPaid()
        {
            throw new NotImplementedException();
        }

        public Task<List<TicketDto>> GetByUserPending()
        {
            throw new NotImplementedException();
        }

        public Task pay(int id)
        {
            throw new NotImplementedException();
        }

        public Task Win(int id)
        {
            throw new NotImplementedException();
        }
    }
}
