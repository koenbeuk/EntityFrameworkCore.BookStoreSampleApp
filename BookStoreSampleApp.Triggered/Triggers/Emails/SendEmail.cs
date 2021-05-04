using BookStoreSampleApp.Common;
using BookStoreSampleApp.Common.Models;
using BookStoreSampleApp.Common.Services;
using EntityFrameworkCore.Triggered;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStoreSampleApp.Triggered.Triggers.Emails
{
    public class SendEmail : IAfterSaveTrigger<Email>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly EmailService _emailService;

        public SendEmail(ApplicationDbContext applicationDbContext, EmailService emailService)
        {
            _applicationDbContext = applicationDbContext;
            _emailService = emailService;
        }

        public async Task AfterSave(ITriggerContext<Email> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Added)
            {
                var customer = _applicationDbContext.Customers.Find(context.Entity.CustomerId);

                _emailService.SendEmail(customer.EmailAddress, customer.DisplayName, context.Entity.Title, context.Entity.Body);

                context.Entity.SentDate = DateTime.Now;

                await _applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
