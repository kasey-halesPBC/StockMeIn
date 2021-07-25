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
    public class DetailsModel : PageModel
    {
        private readonly StockMeIn.Data.StockMeInContext _context;

        public DetailsModel(StockMeIn.Data.StockMeInContext context)
        {
            _context = context;
        }
        // Use customer model
        public Customer Customer { get; set; }
        // Use CustomerVehicle model to display owned vehicles
        public IList<CustomerVehicle> CustomerVehicle { get; set; }
        // User Vehicle model to display owned vehicle details
        public IList<Vehicle> Vehicle { get; set; }
        public IList<CustVehInfo> CustVehInfo { get; set; }
        public CustomerVehicle custVehicle { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {  // If customer ID is null return not found page
                return NotFound();
            }
            // Query customer table to get customer inforamtion
            Customer = await _context.Customer.FirstOrDefaultAsync(m => m.ID == id);

            if (Customer == null)
            { // If customer not found return page not found
                return NotFound();
            }
            // Query to join the customer vehicle table and vehicle table to get vehicle information
            var custVehicles = from cv in _context.CustomerVehicle.Where(c => c.CustID == id)
                               join vi in _context.Vehicle on cv.CustStockID equals vi.ID into cv2
                               from vi in cv2.DefaultIfEmpty()
                               select new CustVehInfo { CustVehData = cv, VehicleInfo = vi };
            // Fetch customer vehicle information from database
            CustVehInfo = await custVehicles.ToListAsync();
            // Return to page
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(EndVehicleRequest request)
        {  
            if (request == null)
            {  // If customer vehcicle request is null return not found page
                return NotFound();
            }
            // Get customer vehicle record from customer vehicle tabler
            custVehicle = await _context.CustomerVehicle.FirstOrDefaultAsync(ev => ev.ID == request.CustVehicleID);

            if (custVehicle == null)
            {  // If not record is found return not found
                return NotFound();
            }
            // Set customer vehicle end data to today
            custVehicle.EndDate = DateTime.Now;
            // Update customer vehicle table
            _context.CustomerVehicle.Update(custVehicle);
            // Await changes
            await _context.SaveChangesAsync();
            // Return to customer infromation page with customer ID route
            return RedirectToPage("./Details", new { id = request.CustID });
        }
    }
}
