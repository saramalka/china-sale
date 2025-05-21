using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Interfaces
{
    public interface IGiftRepository
    {
        Task<IEnumerable<Gift>> GetAllAsync();
        Task<Gift?> GetByIdAsync(int id);
        Task<Gift> AddAsync(Gift gift);
        Task UpdateAsync(Gift gift);
        Task DeleteAsync(int id);
    }
}
