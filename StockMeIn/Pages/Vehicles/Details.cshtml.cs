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
    public class DetailsModel : PageModel
    {
        private readonly StockMeIn.Data.StockMeInContext _context;

        public DetailsModel(StockMeIn.Data.StockMeInContext context)
        {
            _context = context;
        }
        // Define properties used by page
        public Vehicle Vehicle { get; set; }
        public CustomerVehicle CustVehicle { get; set; }
        public IList<Customer> Customer { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        [ViewData]
        public string Message { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {  // If vehicle ID is null return not found page
                return NotFound();
            }

            // Query vehicle table for vehicle information
            Vehicle = await _context.Vehicle.FirstOrDefaultAsync(m => m.ID == id);

            if (Vehicle == null)
            {  // If no vehicle information is found return not found page
                return NotFound();
            }

            // LINQ query to select custmoers for customer table
            var lastNames = from m in _context.Customer
                            select m;

            if (!string.IsNullOrEmpty(SearchString))
            {  // If search string isn't empty select from table where contains string
                lastNames = lastNames.Where(s => s.LastName.Contains(SearchString));
            }

            // Get customers from query
            Customer = await lastNames.ToListAsync();
            // Return to details page
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(SellVehicleRequest request)
        {  // When purchase button is selected
            if (request == null)
            {  // IF customerID and vehicleID is null return not found
                return NotFound();
            }
            // Check for record in CustVehicle table with null end date
            CustVehicle = await _context.CustomerVehicle.FirstOrDefaultAsync(c => c.CustStockID == request.VehicleID &&
                          c.CustID == request.CustomerID && c.EndDate == DateTime.MinValue);

            if (CustVehicle != null)
            {  // If a matching record is found update end date to now.
                CustVehicle.EndDate = DateTime.Now;
                _context.CustomerVehicle.Update(CustVehicle);
                await _context.SaveChangesAsync();
            }
            // Create new cust vehicle record with start date now
            CustomerVehicle custVeh = new CustomerVehicle();
            custVeh.CustID = request.CustomerID;
            custVeh.CustStockID = request.VehicleID;
            custVeh.StartDate = DateTime.Now;
            // Add new record to customer vehicle table
            _context.CustomerVehicle.Add(custVeh);
            await _context.SaveChangesAsync();
            // Get inventory vehicle record and set status to customer
            Vehicle = await _context.Vehicle.FirstOrDefaultAsync(v => v.ID == request.VehicleID);
            // Set vehicle status to customer
            Vehicle.status = 'C';
            // Update vehicle record
            _context.Vehicle.Update(Vehicle);
            // Wait for changes
            await _context.SaveChangesAsync();

            // Redirect to customers info page with customer ID route
            return RedirectToPage("/Customers/Details", new { id = request.CustomerID });
        }
    }
}
