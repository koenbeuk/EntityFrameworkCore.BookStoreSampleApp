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
    public class SendWelcomeEmail : IBeforeSaveTrigger<Customer>
    {
        readonly ApplicationDbContext _applicationDbContext;

        public SendWelcomeEmail(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Task BeforeSave(ITriggerContext<Customer> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Added)
            {
                _applicationDbContext.Emails.Add(new Email
                {
                    Customer = context.Entity,
                    QueueDate = DateTime.Now,
                    Title = "Welcome!",
                    Body = $"We hope you have a great stay at our awesome bookstore!"
                });
            }

            return Task.CompletedTask;
        }
    }
}
