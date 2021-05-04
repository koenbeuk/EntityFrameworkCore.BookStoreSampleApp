using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookStoreSampleApp.Common.Models;
using BookStoreSampleApp.Simple;
using BookStoreSampleApp.Common;

namespace BookStoreSampleApp.Simple.Pages.Emails
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Email = await _context.Emails.FindAsync(id);

            if (Email != null)
            {
                _context.Emails.Remove(Email);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
