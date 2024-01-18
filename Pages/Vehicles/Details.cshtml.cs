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

namespace Service_Auto.Pages.Vehicles
{
    public class DetailsModel : PageModel
    {
        private readonly Service_Auto.Data.Service_AutoContext _context;

        public DetailsModel(Service_Auto.Data.Service_AutoContext context)
        {
            _context = context;
        }

        public Vehicle Vehicle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.VehicleID == id);

            if (vehicle == null)
            {
                return NotFound();
            }
            else
            {
                Vehicle = vehicle;
            }

            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "Name");

            return Page();
        }
    }
}
