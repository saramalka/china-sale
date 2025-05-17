using DL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    { 
        private readonly AppDbContext _context;
        public PurchaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Purchase>> GetAllAsync()
            => await _context.Purchases.ToListAsync();

        public async Task<Purchase?> GetByIdAsync(int id)
            => await _context.Purchases.FindAsync(id);

        public async Task<Purchase> AddAsync(Purchase purchase)
        {
            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();
            return purchase;
        }

        public async Task UpdateAsync(Purchase purchase)
        {
            _context.Purchases.Update(purchase);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase != null)
            {
                _context.Purchases.Remove(purchase);
                await _context.SaveChangesAsync();
            }
        }
    }

}


