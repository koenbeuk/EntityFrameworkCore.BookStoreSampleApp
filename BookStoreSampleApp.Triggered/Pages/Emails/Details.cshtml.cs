using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStoreSampleApp.Common.Models;
using BookStoreSampleApp.Triggered;

namespace BookStoreSampleApp.Triggered.Pages.Emails
{
    public class DetailsModel : PageModel
    {
        private readonly BookStoreSampleApp.Triggered.ApplicationDbContext _context;

        public DetailsModel(BookStoreSampleApp.Triggered.ApplicationDbContext context)
        {
            _context = context;
        }

        public Email Email { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Email = await _context.Emails.FirstOrDefaultAsync(m => m.Id == id);

            if (Email == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
