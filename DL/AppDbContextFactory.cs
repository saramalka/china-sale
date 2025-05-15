﻿using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();


            optionsBuilder.UseSqlServer("Server=DESKTOP-T6871CH;Database=china-sale;Trusted_Connection=True;TrustServerCertificate=True");
            ;


            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
