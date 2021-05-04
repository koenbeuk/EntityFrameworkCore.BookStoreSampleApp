using BookStoreSampleApp.Common;
using BookStoreSampleApp.Common.Models;
using EntityFrameworkCore.Triggered;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStoreSampleApp.Triggered.Triggers.CustomerPurchases
{
    public class CreateEmail : IBeforeSaveTrigger<CustomerPurchase>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CreateEmail(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Task BeforeSave(ITriggerContext<CustomerPurchase> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Added)
            {
                var email = new Email
                {
                    Customer = context.Entity.Customer,
                    QueueDate = DateTime.Now
                };

                var book = context.Entity.Book ?? _applicationDbContext.Books.Find(context.Entity.BookId);

                if (context.Entity.Price == 0)
                {
                    email.Title = "You've got a free book!";
                    email.Body = $"We've just given you {book.Title} for free because we're awesome!";
                }
                else
                {
                    email.Title = "You've just purchased a book!";
                    email.Body = $"You're now the proud owner of {book.Title}!";
                }

                _applicationDbContext.Emails.Add(email);
            }

            return Task.CompletedTask;
        }
    }
}
