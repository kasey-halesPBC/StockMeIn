using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StockMeIn.Models;

namespace StockMeIn.Pages.Vehicles
{
    public class InvCleanupModel : PageModel
    {
        private readonly StockMeIn.Data.StockMeInContext _context;

        public InvCleanupModel(StockMeIn.Data.StockMeInContext context)
        {
            _context = context;
        }

        public IList<Vehicle> Vehicle { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Delete Inventory Before")]
        [Required]
        public DateTime DeleteDate { get; set; }
        // Variable for Message
        [ViewData]
        public string Message { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(DateTime? DeleteDate)
        {
            // Select all inventory records before delete date
            var delVehicles = from d in _context.Vehicle.Where(d => d.inventoryDate < DeleteDate
                              && d.status == "I") select d;
            // Variable to indicate if records were deleted
            bool deleted = false;
            // Variable to count deleted records
            int deleteCount = 0;
            foreach (var veh in delVehicles)
            {  // For each vehicle found before delete date
                if (veh != null)
                {  // If vehicle record is not null update deleted status and update
                    veh.status = "D";
                    _context.Vehicle.Update(veh);
                    // Incremented deleted counter
                    deleteCount++;
                    // Set deleted to true
                    deleted = true;
                }
            }
            // Wait for changes to be saved to the database
            await _context.SaveChangesAsync();
            // If records were deleted set message to deleted
            if (deleted) 
               Message = deleteCount.ToString() + " vehicles stocked in before " + DeleteDate + 
                    " have been set to deleted status.";
            // If no records were deleted display no records found
            else    
                Message = "No inventory before " + DeleteDate + " found for cleanup.";
            // Set delete date to minumum date
            DeleteDate = DateTime.MinValue;
            // Cleare model state
            ModelState.Clear();
            // Redirect to page
            return Page();
        }
    }
}
