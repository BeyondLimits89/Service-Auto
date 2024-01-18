using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Service_Auto.Data;
using Service_Auto.Models;

namespace Service_Auto.Pages.Repairs
{
    public class EditModel : PageModel
    {
        private readonly Service_Auto.Data.Service_AutoContext _context;

        public EditModel(Service_Auto.Data.Service_AutoContext context)
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

            var repair =  await _context.Repair.FirstOrDefaultAsync(m => m.ServiceID == id);
            if (repair == null)
            {
                return NotFound();
            }
            Repair = repair;
           ViewData["VehicleID"] = new SelectList(_context.Set<Vehicle>(), "VehicleID", "Make");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Repair).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RepairExists(Repair.ServiceID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RepairExists(int id)
        {
            return _context.Repair.Any(e => e.ServiceID == id);
        }
    }
}
