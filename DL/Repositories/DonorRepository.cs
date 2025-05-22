using AutoMapper;
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
        public class DonorRepository : IDonorRepository
    {
            private readonly AppDbContext _context;
            private readonly IMapper _mapper;
            public DonorRepository(AppDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task Add(Donor donor)
            {
                _context.Donors.Add(donor);
                await _context.SaveChangesAsync();
            }

            public async Task Delete(int id)
            {
                var donor = await _context.Donors.FindAsync(id);
                if (donor == null)
                {
                    throw new KeyNotFoundException($"Donor with ID {id} not found.");
                }

                _context.Donors.Remove(donor);
                await _context.SaveChangesAsync();

            }

            public async Task<List<DonorDto>> Get()
            {
                var donors = await _context.Donors
                    .Include(d => d.Gifts)
                    .ToListAsync();
                if (donors == null || !donors.Any())
                {
                    throw new InvalidOperationException("No donors found.");
                }

                var donorDtos = _mapper.Map<List<DonorDto>>(donors);
                return donorDtos;
            }

            public async Task<DonorDto> Get(int id)
            {
                var donor = await _context.Donors
                    .Include(d => d.Gifts)
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (donor == null)
                {
                    throw new KeyNotFoundException($"Donor with ID {id} not found.");
                }

                var donorDto = _mapper.Map<DonorDto>(donor);
                return donorDto;
            }

            public async Task Update(int id, DonorDto donorDto)
            {
                var existingDonor = await _context.Donors.FindAsync(id);
                if (existingDonor == null)
                {
                    throw new KeyNotFoundException($"Donor with ID {id} not found.");
                }
                if (existingDonor != null)
                {
                    existingDonor.Name = donorDto.Name;
                    existingDonor.Email = donorDto.Email;
                    existingDonor.ShowMe = donorDto.ShowMe;

                    _context.Donors.Update(existingDonor);
                    await _context.SaveChangesAsync();
                }
            }

            public async Task<List<DonorDto>> Search(string name = null, string email = null, string giftName = null)
            {
                var query = _context.Donors
                    .Include(d => d.Gifts)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(name))
                {
                    query = query.Where(d => d.Name.Contains(name));
                }

                if (!string.IsNullOrWhiteSpace(email))
                {
                    query = query.Where(d => d.Email.Contains(email));
                }

                if (!string.IsNullOrWhiteSpace(giftName))
                {
                    query = query.Where(d => d.Gifts.Any(g => g.GiftName.Contains(giftName)));
                }
                var donors = await query.ToListAsync();
                if (donors == null || !donors.Any())
                {
                    throw new InvalidOperationException("No donors match the search criteria.");
                }
                var donorDtos = _mapper.Map<List<DonorDto>>(donors);
                return donorDtos;
            }
        }

    }
