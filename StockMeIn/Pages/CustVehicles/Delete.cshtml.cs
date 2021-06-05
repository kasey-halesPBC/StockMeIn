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
    public class DeleteModel : PageModel
    {
        private readonly StockMeIn.Data.StockMeInContext _context;

        public DeleteModel(StockMeIn.Data.StockMeInContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CustomerVehicle = await _context.CustomerVehicle.FindAsync(id);

            if (CustomerVehicle != null)
            {
                _context.CustomerVehicle.Remove(CustomerVehicle);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
