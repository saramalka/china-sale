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

            modelBuilder.Entity<Gift>()
                .HasOne(g => g.Donation)
                .WithMany(d => d.Gifts)
                .HasForeignKey(g => g.DonationId)
                .OnDelete(DeleteBehavior.Restrict); // ❌ לא Cascade

            modelBuilder.Entity<Gift>()
                .HasOne(g => g.Winner)
                .WithMany(u => u.WonGifts)
                .HasForeignKey(g => g.WinnerId)
                .OnDelete(DeleteBehavior.SetNull); // ✅ Winner לא חייב להימחק

            modelBuilder.Entity<Donation>()
                .HasOne(d => d.Donor)
                .WithMany() // אם אתה לא צריך קשר חזור אל Donor
                .HasForeignKey(d => d.DonorId)
                .OnDelete(DeleteBehavior.Restrict); // ❌ לא Cascade

            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.User)
                .WithMany(u => u.Purchases)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict); // ❌ לא Cascade

            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.Gift)
                .WithMany(g => g.Purchases)
                .HasForeignKey(p => p.GiftId)
                .OnDelete(DeleteBehavior.Cascade); // ✅ רק כאן ניתן Cascade
        }


    }
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    base.OnModelCreating(modelBuilder);

    //    modelBuilder.Entity<Gift>()
    //        .HasOne(g => g.Donation)
    //        .WithMany(d => d.Gifts)
    //        .HasForeignKey(g => g.DonationId)
    //        .OnDelete(DeleteBehavior.Restrict); // שינוי מ-Cascade ל-Restrict

    //    modelBuilder.Entity<Gift>()
    //        .HasOne(g => g.Winner)
    //        .WithMany(u => u.WonGifts)
    //        .HasForeignKey(g => g.WinnerId)
    //        .OnDelete(DeleteBehavior.SetNull);

    //    modelBuilder.Entity<Donation>()
    //        .HasOne(d => d.Donor)
    //        .WithMany()
    //        .HasForeignKey(d => d.DonorId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    modelBuilder.Entity<Purchase>()
    //        .HasOne(p => p.User)
    //        .WithMany(u => u.Purchases)
    //        .HasForeignKey(p => p.UserId)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    modelBuilder.Entity<Purchase>()
    //        .HasOne(p => p.Gift)
    //        .WithMany(g => g.Purchases)
    //        .HasForeignKey(p => p.GiftId)
    //        .OnDelete(DeleteBehavior.Cascade); 
    //}

}
