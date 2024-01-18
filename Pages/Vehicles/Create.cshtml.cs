using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service_Auto.Data;
using Service_Auto.Models;

namespace Service_Auto.Pages.Vehicles
{
    public class CreateModel : PageModel
    {
        private readonly Service_Auto.Data.Service_AutoContext _context;

        public CreateModel(Service_Auto.Data.Service_AutoContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "Name");
            return Page();
        }

        [BindProperty]
        public Vehicle Vehicle { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();            
            }

            _context.Vehicle.Add(Vehicle);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
