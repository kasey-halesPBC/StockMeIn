using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockMeIn.Models;

namespace StockMeIn.Data
{
    public class StockMeInContext : DbContext
    {
        public StockMeInContext (DbContextOptions<StockMeInContext> options)
            : base(options)
        {
        }

        public DbSet<StockMeIn.Models.Vehicle> Vehicle { get; set; }

        public DbSet<StockMeIn.Models.Customer> Customer { get; set; }
    }
}
