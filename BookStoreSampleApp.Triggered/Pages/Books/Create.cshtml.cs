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

namespace BookStoreSampleApp.Triggered.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly BookStoreSampleApp.Triggered.ApplicationDbContext _context;
        private readonly EmailService _emailService;

        public CreateModel(BookStoreSampleApp.Triggered.ApplicationDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Books.Add(Book);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
