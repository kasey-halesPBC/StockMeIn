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
    public class DetailsModel : PageModel
    {
        private readonly StockMeIn.Data.StockMeInContext _context;

        public DetailsModel(StockMeIn.Data.StockMeInContext context)
        {
            _context = context;
        }

        public CustomerVehicle CustomerVehicle { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CustomerVehicle = await _context.CustomerVehicle.FirstOrDefaultAsync(m => m.ID == id);

            if (CustomerVehicle == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
