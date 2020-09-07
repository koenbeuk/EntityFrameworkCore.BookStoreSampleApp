using BookStoreSampleApp.Common.Models;
using EntityFrameworkCore.Triggered;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStoreSampleApp.Triggered.Triggers.Books
{
    public class AutoPurchaseFreeBookForAllCustomers : IBeforeSaveTrigger<Book>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AutoPurchaseFreeBookForAllCustomers(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Task BeforeSave(ITriggerContext<Book> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Added || context.ChangeType == ChangeType.Modified)
            {
                if (context.Entity.Price == 0)
                {
                    var customers = _applicationDbContext.Customers
                         .Where(x => x.Purchases.Any(p => p.Book == context.Entity) == false)
                         .ToList();

                    foreach (var customer in customers)
                    {
                        _applicationDbContext.CustomerPurchases.Add(new CustomerPurchase
                        {
                            Book = context.Entity,
                            Customer = customer,
                            Price = 0,
                            PurchaseDate = DateTime.Now
                        });
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
