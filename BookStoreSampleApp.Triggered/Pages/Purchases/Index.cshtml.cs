﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStoreSampleApp.Common.Models;
using BookStoreSampleApp.Triggered;

namespace BookStoreSampleApp.Triggered.Pages.Purchases
{
    public class IndexModel : PageModel
    {
        private readonly BookStoreSampleApp.Triggered.ApplicationDbContext _context;

        public IndexModel(BookStoreSampleApp.Triggered.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CustomerPurchase> CustomerPurchase { get;set; }

        public async Task OnGetAsync()
        {
            CustomerPurchase = await _context.CustomerPurchases
                .Include(c => c.Book)
                .Include(c => c.Customer).ToListAsync();
        }
    }
}
