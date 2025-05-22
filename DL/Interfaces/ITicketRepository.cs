using DL.Entities;
using common.Dto;


namespace DL.Interfaces
{
    public interface ITicketRepository
    {
        Task<List<TicketDto>> Get();
        Task<List<TicketDto>> GetByUserPaid();
        Task<List<TicketDto>> GetByUserPending();
        Task<TicketDto> Get(int id);
        Task Add(Ticket ticket);
        Task pay(int id);
        Task Win(int id);
        Task Delete(int id);
    }
}
