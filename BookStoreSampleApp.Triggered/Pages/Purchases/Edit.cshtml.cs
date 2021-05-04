using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStoreSampleApp.Common.Models;
using BookStoreSampleApp.Triggered;
using BookStoreSampleApp.Common;

namespace BookStoreSampleApp.Triggered.Pages.Purchases
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
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
           ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id");
           ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");
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

            _context.Attach(CustomerPurchase).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerPurchaseExists(CustomerPurchase.Id))
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

        private bool CustomerPurchaseExists(int id)
        {
            return _context.CustomerPurchases.Any(e => e.Id == id);
        }
    }
}
