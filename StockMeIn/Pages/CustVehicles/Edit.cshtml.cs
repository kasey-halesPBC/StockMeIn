using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StockMeIn.Data;
using StockMeIn.Models;

namespace StockMeIn.Pages.CustVehicles
{
    public class EditModel : PageModel
    {
        private readonly StockMeIn.Data.StockMeInContext _context;

        public EditModel(StockMeIn.Data.StockMeInContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(CustomerVehicle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerVehicleExists(CustomerVehicle.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CustomerVehicleExists(int id)
        {
            return _context.CustomerVehicle.Any(e => e.ID == id);
        }
    }
}
