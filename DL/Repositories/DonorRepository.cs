using DL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Interfaces
{

    namespace DL.Interfaces
    {
        public class DonorRepository : IDonorRepository
        {
            private readonly AppDbContext _context;

            public DonorRepository(AppDbContext context)
            {
                _context = context;
            }

            public async Task<Donor> AddAsync(Donor donor)
            {
                await _context.Donors.AddAsync(donor);
                await _context.SaveChangesAsync();
                return donor;
            }

            public async Task<IEnumerable<Donor>> GetAllAsync()
            {
                return await _context.Donors.ToListAsync();
            }

            public async Task<Donor?> GetByIdAsync(int id)
            {
                return await _context.Donors.FindAsync(id);
            }

            public async Task UpdateAsync(Donor donor)
            {
                _context.Donors.Update(donor);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(int id)
            {
                var donor = await _context.Donors.FindAsync(id);
                if (donor != null)
                {
                    _context.Donors.Remove(donor);
                    await _context.SaveChangesAsync();
                }
            }


        }

    }
}
