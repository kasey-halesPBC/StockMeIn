using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StockMeIn.Data;
using StockMeIn.Models;

namespace StockMeIn.Pages.Customers
{
    public class DeleteModel : PageModel
    {
        private readonly StockMeIn.Data.StockMeInContext _context;

        public DeleteModel(StockMeIn.Data.StockMeInContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {  // If ID is null return not found
                return NotFound();
            }
            // Link query to get customer record
            Customer = await _context.Customer.FirstOrDefaultAsync(m => m.ID == id);

            if (Customer == null)
            {  // If customer not found return not found
                return NotFound();
            }
            // Return to current page
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {  // If id is null return not found
                return NotFound();
            }
            // Get customer record
            Customer = await _context.Customer.FindAsync(id);

            if (Customer != null)
            {  // If customer record is not null, remove customer from database and await changes
                _context.Customer.Remove(Customer);
                await _context.SaveChangesAsync();
            }
            // Redirect to customers page
            return RedirectToPage("./Index");
        }
    }
}
