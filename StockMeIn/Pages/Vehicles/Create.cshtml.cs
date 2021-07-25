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

namespace StockMeIn.Pages.Vehicles
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
        public Vehicle Vehicle { get; set; }
        public CustomerVehicle CustomerVehicle { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {  // If state is not valid return to page
                return Page();
            }

            // Query to check if vehicle already exists in vehicle table
            var vehCheck = await _context.Vehicle.FirstOrDefaultAsync(m => m.VIN == Vehicle.VIN);

            if (vehCheck != null)
            {  // If vehicle does exist
                // Set status to inventory
                Vehicle = vehCheck;
                Vehicle.status = 'I';
                // Set inventory date to now
                Vehicle.inventoryDate = DateTime.Now;
                // Get vehicle ID for customer vehicle check
                var id = Vehicle.ID;
                var msgVin = Vehicle.VIN;
                // Update vehicle record
                _context.Vehicle.Update(Vehicle);
                // Wait for changes
                await _context.SaveChangesAsync();
                // Query to check if vehicle is assigned to a customer
                CustomerVehicle = await _context.CustomerVehicle.FirstOrDefaultAsync(cv => cv.CustStockID == id
                                  && cv.EndDate == DateTime.MinValue);

                if (CustomerVehicle != null)
                {  // If vehicle is assigned to a customer set end date to now
                    CustomerVehicle.EndDate = DateTime.Now;
                    // Update customer vehicle record
                    _context.CustomerVehicle.Update(CustomerVehicle);
                    // Wait for changes
                    await _context.SaveChangesAsync();
                }
                Message = Vehicle.VIN + " was owned by a customer. Converted to inventory.";

            }
            else
            {  // Else if vehicle doesn't exist add vehicle record
                _context.Vehicle.Add(Vehicle);
                await _context.SaveChangesAsync();
                Message = Vehicle.VIN + " was added to inventory.";
            }
            // Clear vehicle fields
            Vehicle.VIN = null;
            Vehicle.typeNU = null;
            Vehicle.year = 2000;
            Vehicle.make = null;
            Vehicle.model = null;
            Vehicle.body = null;
            Vehicle.color = null;
            Vehicle.inventoryDate = DateTime.MinValue;
            Vehicle.cost = 0;
            Vehicle.salePrice = 0;
            Vehicle.status = 'I';
            // Clear model state
            ModelState.Clear();
            // Return to vehicle page
            return Page();
        }
    }
}
