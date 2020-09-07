﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStoreSampleApp.Common.Models;
using BookStoreSampleApp.Simple;

namespace BookStoreSampleApp.Simple.Pages.Emails
{
    public class IndexModel : PageModel
    {
        private readonly BookStoreSampleApp.Simple.ApplicationDbContext _context;

        public IndexModel(BookStoreSampleApp.Simple.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Email> Email { get;set; }

        public async Task OnGetAsync()
        {
            Email = await _context.Emails.ToListAsync();
        }
    }
}