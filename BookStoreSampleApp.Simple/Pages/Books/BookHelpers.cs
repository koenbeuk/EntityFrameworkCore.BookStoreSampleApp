using BookStoreSampleApp.Common;
using BookStoreSampleApp.Common.Models;
using BookStoreSampleApp.Common.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreSampleApp.Simple.Pages.Books
{
    public class BookHelpers
    {
        private readonly List<Email> _queuedEmails = new List<Email>();

        public void AutoPurchaseBookForAllCustomers(ApplicationDbContext context, Book book)
        {
            var customers = context.Customers
                .Where(x => x.Purchases.Any(p => p.Book == book) == false)
                .ToList();

            foreach (var customer in customers)
            {
                context.CustomerPurchases.Add(new CustomerPurchase
                {
                    Book = book,
                    Customer = customer,
                    Price = 0,
                    PurchaseDate = DateTime.Now
                });

                var email = new Email
                {
                    Customer = customer,
                    QueueDate = DateTime.Now,
                    Title = "You've got a free book!",
                    Body = $"We've just given you {book.Title} for free because we're awesome!"
                };

                context.Emails.Add(email);
                _queuedEmails.Add(email);
            }
        }

        public void SendQueuedEmails(EmailService emailService)
        {
            foreach (var email in _queuedEmails)
            {
                emailService.SendEmail(email.Customer.EmailAddress, email.Customer.DisplayName, email.Title, email.Body);
                email.SentDate = DateTime.Now;
            }
        }
    }
}
