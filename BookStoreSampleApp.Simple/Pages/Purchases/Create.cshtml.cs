using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookStoreSampleApp.Common.Models;
using BookStoreSampleApp.Simple;
using BookStoreSampleApp.Common.Services;

namespace BookStoreSampleApp.Simple.Pages.Purchases
{
    public class CreateModel : PageModel
    {
        private readonly BookStoreSampleApp.Simple.ApplicationDbContext _context;
        private readonly EmailService _emailService;

        public CreateModel(BookStoreSampleApp.Simple.ApplicationDbContext context, EmailService emailService)
        {
            _context = context;
            this._emailService = emailService;
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

            var customer = _context.Customers.Find(CustomerPurchase.CustomerId);
            var book = _context.Books.Find(CustomerPurchase.BookId);

            var email = new Email
            {
                CustomerId = customer.Id,
                QueueDate = DateTime.Now,
                Title = "You've just purchased a book!",
                Body = $"You're now the proud owner of {book.Title}!"
            };

            _context.Emails.Add(email);

            await _context.SaveChangesAsync();

            _emailService.SendEmail(customer.EmailAddress, customer.DisplayName, email.Title, email.Body);

            return RedirectToPage("./Index");
        }
    }
}
