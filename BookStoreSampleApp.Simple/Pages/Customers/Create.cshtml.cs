using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookStoreSampleApp.Common.Models;
using BookStoreSampleApp.Simple;
using System.Net.WebSockets;
using BookStoreSampleApp.Common.Services;
using BookStoreSampleApp.Common;

namespace BookStoreSampleApp.Simple.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailService _emailService;

        public CreateModel(ApplicationDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Customers.Add(Customer);

            Customer.SignupDate = DateTime.Now;

            var freeBooks = _context.Books
                .Where(x => x.Price == 0)
                .ToList();

            var queuedEmails = new List<Email>();

            var welcomeEmail = new Email
            {
                Customer = Customer,
                QueueDate = DateTime.Now,
                Title = "Welcome!",
                Body = $"We hope you have a great stay at our awesome bookstore!"
            };

            _context.Emails.Add(welcomeEmail);
            queuedEmails.Add(welcomeEmail);

            foreach (var book in freeBooks)
            {
                _context.CustomerPurchases.Add(new CustomerPurchase
                {
                    Book = book,
                    Customer = Customer,
                    Price = 0,
                    PurchaseDate = DateTime.Now
                });

                var email = new Email
                {
                    Customer = Customer,
                    QueueDate = DateTime.Now,
                    Title = "You've got a free book!",
                    Body = $"We've just given you {book.Title} for free because we're awesome!"
                };

                _context.Emails.Add(email);
                queuedEmails.Add(email);
            }

            await _context.SaveChangesAsync();

            foreach (var email in queuedEmails)
            {
                _emailService.SendEmail(Customer.EmailAddress, Customer.DisplayName, email.Title, email.Body);
                email.SentDate = DateTime.Now;
            }

            return RedirectToPage("./Index");
        }
    }
}
