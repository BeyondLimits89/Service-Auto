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
    public class DetailsModel : PageModel
    {
        private readonly Service_Auto.Data.Service_AutoContext _context;

        public DetailsModel(Service_Auto.Data.Service_AutoContext context)
        {
            _context = context;
        }

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
    }
}
