﻿using AutoMapper;
using common.enums;
using common.Dto;
using DL.Entities;
using DL.Interfaces;
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
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public GiftRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<GiftDto>> Get()
        {
            var gifts = await _context.Gifts
                .Include(g => g.Category)
                .Include(g => g.Donor)
                .Include(g => g.Tickets.Where(t => t.Status != TicketStatus.Pending))
                .Include(g => g.Winner)
                .ToListAsync();

            if (gifts == null || !gifts.Any())
                throw new InvalidOperationException("No gifts found.");

            return _mapper.Map<List<GiftDto>>(gifts);
        }
        public async Task<Gift> Get(int id)
        {
            var gift = await _context.Gifts
                .Include(g => g.Category)
                .Include(g => g.Donor)
                .Include(g => g.Tickets.Where(t => t.Status != TicketStatus.Pending))
                .Include(g => g.Winner)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (gift == null)
                throw new KeyNotFoundException($"Gift with ID {id} not found.");

            return gift;

        }
        public async Task Add(Gift gift)
        {
            var existCategory = await _context.Categories.FindAsync(gift.CategoryId);
            if (existCategory == null)
                throw new InvalidDataException($"Category with ID {gift.CategoryId} not found.");
            var existDonor = await _context.Donors.FindAsync(gift.DonorId);
            if (existDonor == null)
                throw new InvalidDataException($"Donor with ID {gift.DonorId} not found.");
            if (gift.WinnerId != null)
            {
                var existWinner = await _context.Users.FindAsync(gift.WinnerId);
                if (existWinner == null)
                    throw new InvalidDataException($"Winner with ID {gift.WinnerId} not found.");
            }
            var existingGift = await TitleExists(gift.GiftName);
            if (existingGift)
            {
                throw new InvalidOperationException("Gift with this name already exists.");
            }
            var giftDB=_mapper.Map<Gift>(gift);
            await _context.Gifts.AddAsync(giftDB);
            await _context.SaveChangesAsync();
        }
        public async Task Update(int id, GiftDto gift)
        {
            var existingGift = await _context.Gifts.FindAsync(id);
            if (existingGift == null) throw new KeyNotFoundException($"Gift with ID {id} not found.");
            var duplicate = await _context.Gifts.AnyAsync(g => g.GiftName == gift.GiftName && g.Id != id);
            if (duplicate)
            {
                throw new InvalidOperationException($"Gift with name {gift.GiftName} already exists.");
            }
            var existCategory = await _context.Categories.FindAsync(gift.CategoryId);
            if (existCategory == null)
                throw new InvalidDataException($"Category with ID {gift.CategoryId} not found.");
            var existDonor = await _context.Donors.FindAsync(gift.DonorId);
            if (existDonor == null)
                throw new InvalidDataException($"Donor with ID {gift.DonorId} not found.");
            if (gift.WinnerId != null)
            {
                var existWinner = await _context.Users.FindAsync(gift.WinnerId);
                if (existWinner == null)
                    throw new InvalidDataException($"Winner with ID {gift.WinnerId} not found.");
            }
            existingGift.GiftName = gift.GiftName;
            existingGift.Details = gift.Details;
            existingGift.Price = gift.Price;
            existingGift.CategoryId = gift.CategoryId;
            existingGift.DonorId = gift.DonorId;
            existingGift.WinnerId = gift.WinnerId;
            existingGift.ImageUrl = gift.ImageUrl;
            await _context.SaveChangesAsync();
        }
        public async Task<bool> Delete(int id)
        {
            var gift = await _context.Gifts.FindAsync(id);
            if (gift == null)
                throw new KeyNotFoundException($"Gift with ID {id} not found.");

            if (gift == null) return false;
            _context.Gifts.Remove(gift);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<GiftDto>> Search(string giftName = null, string donorName = null, int? buyerCount = null)
        {
            var query = _context.Gifts
                .Include(g => g.Category)
                .Include(g => g.Donor)
                .Include(g => g.Tickets.Where(t => t.Status != TicketStatus.Pending))
                .Include(g => g.Winner)
                .AsQueryable();

            if (!string.IsNullOrEmpty(giftName))
            {
                query = query.Where(g => g.GiftName.Contains(giftName));
            }

            if (!string.IsNullOrEmpty(donorName))
            {
                query = query.Where(g => g.Donor.Name.Contains(donorName));
            }

            if (buyerCount.HasValue)
            {
                query = query.Where(g => g.Tickets.Count == buyerCount);
            }
            var gifts = await query.ToListAsync();
            var giftsDtos = _mapper.Map<List<GiftDto>>(gifts);
            return giftsDtos;
        }
        public async Task<DonorDto> GetDonor(int giftId)
        {
            var gift = await _context.Gifts
                .Include(g => g.Donor)
                .FirstOrDefaultAsync(g => g.Id == giftId);
            if (gift == null)
                throw new KeyNotFoundException($"Gift with ID {giftId} not found.");

            var donorDto = _mapper.Map<DonorDto>(gift.Donor);
            return donorDto;
        }
        public async Task<bool> TitleExists(string title)
        {
            return await _context.Gifts.AnyAsync(g => g.GiftName == title);
        }
        public async Task<List<GiftDto>> SortByPrice()
        {
            var gifts = await _context.Gifts
                .Include(g => g.Category)
                .Include(g => g.Donor)
                .Include(g => g.Tickets.Where(t => t.Status != TicketStatus.Pending))
                .Include(g => g.Winner)
                .OrderBy(g => g.Price)
                .ToListAsync();

            if (gifts == null || !gifts.Any())
                throw new InvalidOperationException("No gifts found to sort by price.");

            return _mapper.Map<List<GiftDto>>(gifts);
        }
        public async Task<List<GiftDto>> SortByCategory()
        {
            var gifts = await _context.Gifts
                .Include(g => g.Category)
                .Include(g => g.Donor)
                .Include(g => g.Tickets.Where(t => t.Status != TicketStatus.Pending))
                .Include(g => g.Winner)
                .OrderBy(g => g.Category.Name)
                .ToListAsync();

            if (gifts == null || !gifts.Any())
                throw new InvalidOperationException("No gifts found to sort by price.");
            return _mapper.Map<List<GiftDto>>(gifts);
        }

        public async Task UpdateWinnerId(int id, int winnerId)
        {
            var gift = await _context.Gifts.FindAsync(id);
            if (gift == null)
                throw new KeyNotFoundException($"Gift with ID {id} not found.");
            var winner = await _context.Users.FindAsync(winnerId);
            if (winner == null)
                throw new InvalidDataException($"Winner with ID {winnerId} not found.");
            gift.WinnerId = winnerId;
            _context.Gifts.Update(gift);
            await _context.SaveChangesAsync();
        }
    }
}
