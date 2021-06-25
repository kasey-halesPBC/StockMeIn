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
    public class IndexModel : PageModel
    {
        private readonly StockMeIn.Data.StockMeInContext _context;

        public IndexModel(StockMeIn.Data.StockMeInContext context)
        {
            _context = context;
        }

        public IList<Customer> Customer { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            // LINQ query to select custmoers
            var lastNames = from m in _context.Customer
                         select m;

            if (!string.IsNullOrEmpty(SearchString))
            {  // If search string isn't empty select from table where contains string
                lastNames = lastNames.Where(s => s.LastName.Contains(SearchString));
            }

            Customer = await lastNames.ToListAsync();
        }
    }
}
