using BookStoreSampleApp.Common.Models;
using EntityFrameworkCore.Triggered;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStoreSampleApp.Triggered.Triggers.Customers
{
    public class AutoPurchaseFreeBooks : IBeforeSaveTrigger<Customer>
    {
        private readonly ApplicationDbContext _applicationDbcontext;

        public AutoPurchaseFreeBooks(ApplicationDbContext applicationDbcontext)
        {
            _applicationDbcontext = applicationDbcontext;
        }

        public Task BeforeSave(ITriggerContext<Customer> context, CancellationToken cancellationToken)
        {
            var freeBooks = _applicationDbcontext.Books
                .Where(x => x.Price == 0)
                .ToList();

            foreach (var book in freeBooks)
            {
                _applicationDbcontext.CustomerPurchases.Add(new CustomerPurchase
                {
                    Book = book,
                    Customer = context.Entity,
                    Price = 0,
                    PurchaseDate = DateTime.Now
                });
            }

            return Task.CompletedTask;
        }
    }
}
