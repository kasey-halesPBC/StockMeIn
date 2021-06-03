using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StockMeIn.Data;
using StockMeIn.Models;

namespace StockMeIn.Pages.Vehicles
{
    public class IndexModel : PageModel
    {
        private readonly StockMeIn.Data.StockMeInContext _context;

        public IndexModel(StockMeIn.Data.StockMeInContext context)
        {
            _context = context;
        }

        public IList<Vehicle> Vehicle { get;set; }

        public async Task OnGetAsync()
        {
            Vehicle = await _context.Vehicle.ToListAsync();
        }
    }
}
