using BookStoreSampleApp.Common.Models;
using EntityFrameworkCore.Triggered;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStoreSampleApp.Triggered.Triggers.Customers
{
    public class SetSignupDate : IBeforeSaveTrigger<Customer>
    {
        public Task BeforeSave(ITriggerContext<Customer> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Added)
            {
                context.Entity.SignupDate = DateTime.Now;
            }
            
            return Task.CompletedTask;
        }
    }
}
