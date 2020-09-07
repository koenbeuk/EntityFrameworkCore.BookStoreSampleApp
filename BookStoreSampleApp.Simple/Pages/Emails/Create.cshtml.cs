using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookStoreSampleApp.Common.Models;
using BookStoreSampleApp.Simple;

namespace BookStoreSampleApp.Simple.Pages.Emails
{
    public class CreateModel : PageModel
    {
        private readonly BookStoreSampleApp.Simple.ApplicationDbContext _context;

        public CreateModel(BookStoreSampleApp.Simple.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Email Email { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Emails.Add(Email);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
