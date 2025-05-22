using common.Dto;
using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketDto>> Get();
        Task<IEnumerable<TicketDto>> GetByUserPaid();
        Task<IEnumerable<TicketDto>> GetByUserPending();
        Task<TicketDto> Get(int id);
        Task Add(Ticket ticket);
        Task pay(int id);
        Task Delete(int id);
    }
}
