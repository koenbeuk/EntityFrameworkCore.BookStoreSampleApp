using BookStoreSampleApp.Common.Models;
using EntityFrameworkCore.Triggered;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreSampleApp.Triggered
{
    public class ApplicationDbContext : TriggeredDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<CustomerPurchase> CustomerPurchases { get; set; }

        public DbSet<Email> Emails { get; set; }
    }
}
