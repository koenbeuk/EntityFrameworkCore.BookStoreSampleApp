using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookStoreSampleApp.Common.Models;
using BookStoreSampleApp.Triggered;
using BookStoreSampleApp.Common.Services;

namespace BookStoreSampleApp.Triggered.Pages.Purchases
{
    public class CreateModel : PageModel
    {
        private readonly BookStoreSampleApp.Triggered.ApplicationDbContext _context;

        public CreateModel(BookStoreSampleApp.Triggered.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id");
        ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public CustomerPurchase CustomerPurchase { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.CustomerPurchases.Add(CustomerPurchase);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
