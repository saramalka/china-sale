using DL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Repositories
{
    public class GiftRepository : IGiftRepository
    {
        private static readonly AppDbContext _context;
        public GiftRepository(AppDbContext context)
        {
            _context= context;
        }
        public async Task<Gift> AddAsync(Gift gift)
        {
            await _context.Gifts.AddAsync(gift);
            return gift;
        }

        public async Task DeleteAsync(int id)
        {
           var gift= await _context.Gifts.FindAsync(id);
            if(gift != null)
            {
                _context.Gifts.Remove(gift);
                await _context.SaveChangesAsync();                                  
            }
           
        }
        public async Task<IEnumerable<Gift>> GetAllAsync()
        {
            var gifts= await _context.Gifts.ToListAsync();
            return gifts;                   
        }

        public async Task<Gift?> GetByIdAsync(int id)
            =>await _context.Gifts.FindAsync(id);
        


        public async Task UpdateAsync(Gift gift)
        {
             _context.Gifts.Update(gift);
            await _context.SaveChangesAsync();

        }
    }
}
