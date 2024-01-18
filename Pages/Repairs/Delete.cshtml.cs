using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Service_Auto.Data;
using Service_Auto.Models;

namespace Service_Auto.Pages.Repairs
{
    public class DeleteModel : PageModel
    {
        private readonly Service_Auto.Data.Service_AutoContext _context;

        public DeleteModel(Service_Auto.Data.Service_AutoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Repair Repair { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repair = await _context.Repair
                .Include(s => s.Vehicle).ThenInclude(v => v.Customer)
                .FirstOrDefaultAsync(m => m.ServiceID == id);

            if (repair == null)
            {
                return NotFound();
            }
            else
            {
                Repair = repair;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repair = await _context.Repair.FindAsync(id);
            if (repair != null)
            {
                Repair = repair;
                _context.Repair.Remove(Repair);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
