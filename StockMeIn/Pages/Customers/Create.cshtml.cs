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

        [ViewData]
        public string Message { get; set; }

        [BindProperty]
        public Customer Customer { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {  // If model state is not valid return page
                return Page();
            }
            // Add customer
            _context.Customer.Add(Customer);
            // Wait for add to complete
            await _context.SaveChangesAsync();
            // Set message that cusomer was added
            if (Customer.FirstName != null)
            {  // If first name is not null set message to last, first
                Message = Customer.LastName + ", " + Customer.FirstName + " was added to customer list.";
            }
            else
            {  // Else set message to last name
                Message = Customer.LastName + " was added to customer list.";
            }
            // Clear customer data
            Customer.LastName = null;
            Customer.FirstName = null;
            Customer.MiddleName = null;
            Customer.Address = null;
            Customer.City = null;
            Customer.State = null;
            Customer.Zip = null;
            Customer.HomePhone = null;
            Customer.WorkPhone = null;
            Customer.CellPhone = null;
            Customer.Notes = null;
            Customer.CreateDAte = DateTime.MinValue;
            Customer.Email = null;
            // Clear model
            ModelState.Clear();
            // Return to customer page
            return Page();
        }
    }
}
