using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StockMeIn.Data;
using StockMeIn.Models;

namespace StockMeIn.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly StockMeIn.Data.StockMeInContext _context;

        public CreateModel(StockMeIn.Data.StockMeInContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Add customer
            _context.Customer.Add(Customer);
            await _context.SaveChangesAsync();
            // Return to customer page
            return RedirectToPage("./Index");
        }
    }
}
