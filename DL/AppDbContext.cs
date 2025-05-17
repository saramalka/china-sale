using DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<Donation>()
                .HasOne(d => d.Donor)
                .WithMany(donor => donor.Donations)
                .HasForeignKey(d => d.DonorId)
                .OnDelete(DeleteBehavior.Restrict); 

            
            modelBuilder.Entity<Gift>()
                .HasOne(g => g.Donation)
                .WithMany(d => d.Gifts)
                .HasForeignKey(g => g.DonationId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Gift>()
                .HasOne(g => g.Winner)
                .WithMany(u => u.WonGifts)
                .HasForeignKey(g => g.WinnerId)
                .OnDelete(DeleteBehavior.SetNull);

            
            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.User)
                .WithMany(u => u.Purchases)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);
           
            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.Gift)
                .WithMany(g => g.Purchases)
                .HasForeignKey(p => p.GiftId)
                .OnDelete(DeleteBehavior.Cascade); 
        }

    }

}
