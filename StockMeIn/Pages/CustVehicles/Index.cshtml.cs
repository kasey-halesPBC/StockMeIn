using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StockMeIn.Data;
using StockMeIn.Models;

namespace StockMeIn.Pages.CustVehicles
{
    public class IndexModel : PageModel
    {
        private readonly StockMeIn.Data.StockMeInContext _context;

        public IndexModel(StockMeIn.Data.StockMeInContext context)
        {
            _context = context;
        }

        public IList<CustomerVehicle> CustomerVehicle { get;set; }

        public async Task OnGetAsync()
        {
            CustomerVehicle = await _context.CustomerVehicle.ToListAsync();
        }
    }
}
