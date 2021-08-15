using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockMeIn.Models;

namespace StockMeIn.Data
{
    public class StockMeInContext : DbContext
    {  // Constructor
        public StockMeInContext (DbContextOptions<StockMeInContext> options)
            : base(options)
        {
        }
        // Vehicle database context
        public DbSet<StockMeIn.Models.Vehicle> Vehicle { get; set; }
        // Customer database context
        public DbSet<StockMeIn.Models.Customer> Customer { get; set; }
        // CustomerVehicle database context
        public DbSet<StockMeIn.Models.CustomerVehicle> CustomerVehicle { get; set; }

        
    }
}
