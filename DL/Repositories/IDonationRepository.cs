using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Repositories
{
    public interface IDonationRepository
    {
        Task<IEnumerable<Donation>> GetAllAsync();
        Task<Donation?> GetByIdAsync(int id);
        Task<Donation> AddAsync(Donation donation);
        Task UpdateAsync(Donation donation);
        Task DeleteAsync(int id);
    }
}
