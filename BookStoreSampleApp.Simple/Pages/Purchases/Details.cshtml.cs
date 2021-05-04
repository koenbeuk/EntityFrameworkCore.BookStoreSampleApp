using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStoreSampleApp.Common.Models;
using BookStoreSampleApp.Simple;
using BookStoreSampleApp.Common;

namespace BookStoreSampleApp.Simple.Pages.Purchases
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public CustomerPurchase CustomerPurchase { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CustomerPurchase = await _context.CustomerPurchases
                .Include(c => c.Book)
                .Include(c => c.Customer).FirstOrDefaultAsync(m => m.Id == id);

            if (CustomerPurchase == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
