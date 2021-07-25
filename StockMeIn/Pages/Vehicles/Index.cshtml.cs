using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StockMeIn.Data;
using StockMeIn.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StockMeIn.Pages.Vehicles
{
    public class IndexModel : PageModel
    {
        private readonly StockMeIn.Data.StockMeInContext _context;

        public IndexModel(StockMeIn.Data.StockMeInContext context)
        {
            _context = context;
        }

        public IList<Vehicle> Vehicle { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public SelectList Types { get; set; }
        [BindProperty(SupportsGet = true)]
        public string VehicleType { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool IsChecked { get; set; }

        public async Task OnGetAsync()
        {
            // LINQ query to get list of vehicle types for type select dropdown.
            IQueryable<string> typeQuery = from m in _context.Vehicle
                                            orderby m.typeNU
                                            select m.typeNU;

            // General LINQ query for all vehicles
            var models = from m in _context.Vehicle select m;

            // LINQ query to select models
            if (IsChecked)
            {  // if customer vehicles is checked run for status of 'C'
                models = from m in _context.Vehicle.Where(m => m.status == 'C')
                             select m;
            }
            else
            {  // If not checked run for status of 'I'
                models = from m in _context.Vehicle.Where(m => m.status == 'I')
                             select m;
            }

            // If search string isn't empty select from table where contains string
            if (IsChecked)
            {  // If customer vehicles is checked
                if (!string.IsNullOrEmpty(SearchString))
                {  // If search box is not empty search for model string
                    models = models.Where(s => s.model.Contains(SearchString)
                    && s.status == 'C');
                }

                if (!string.IsNullOrEmpty(VehicleType))
                {  // Vehicle type is select search for type selected
                    models = models.Where(x => x.typeNU == VehicleType 
                    && x.status == 'C');
                }
            }
            else
            {  // If customer vehicle not checked
                if (!string.IsNullOrEmpty(SearchString))
                {  // If search box is not empty search for model string
                    models = models.Where(s => s.model.Contains(SearchString)
                    && s.status == 'I');
                }

                if (!string.IsNullOrEmpty(VehicleType))
                {  // Vehicle type is select search for type selected
                    models = models.Where(x => x.typeNU == VehicleType
                    && x.status == 'I');
                }
            }

            // Populate type selction drop down with types from database
            Types = new SelectList(await typeQuery.Distinct().ToListAsync());
            // Get vehicle data from database
            Vehicle = await models.ToListAsync();
        }
    }
}
