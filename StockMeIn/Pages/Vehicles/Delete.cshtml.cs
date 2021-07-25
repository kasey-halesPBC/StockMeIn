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
    public class DeleteModel : PageModel
    {
        private readonly StockMeIn.Data.StockMeInContext _context;

        public DeleteModel(StockMeIn.Data.StockMeInContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Vehicle Vehicle { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        { 
            if (id == null)
            {  // If id is null return not found
                return NotFound();
            }
            // Get vehicle data based on id
            Vehicle = await _context.Vehicle.FirstOrDefaultAsync(m => m.ID == id);

            if (Vehicle == null)
            {  // if not found return not found
                return NotFound();
            }
            // Return to page
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {  // If there is no ID sent return not found
                return NotFound();
            }
            // Check for existence of vehcicle in table
            Vehicle = await _context.Vehicle.FindAsync(id);

            if (Vehicle != null)
            {  // If vehicle is found set status to 'D' for deleted
                Vehicle.status = 'D';
                // Update table
                _context.Vehicle.Update(Vehicle);
                // Wait for update to finish
                await _context.SaveChangesAsync();
            }
            // Return to vehicles page
            return RedirectToPage("./Index");
        }
    }
}
