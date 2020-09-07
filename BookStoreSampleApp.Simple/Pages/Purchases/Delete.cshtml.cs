using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStoreSampleApp.Common.Models;
using BookStoreSampleApp.Simple;

namespace BookStoreSampleApp.Simple.Pages.Purchases
{
    public class DeleteModel : PageModel
    {
        private readonly BookStoreSampleApp.Simple.ApplicationDbContext _context;

        public DeleteModel(BookStoreSampleApp.Simple.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CustomerPurchase = await _context.CustomerPurchases.FindAsync(id);

            if (CustomerPurchase != null)
            {
                _context.CustomerPurchases.Remove(CustomerPurchase);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
