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
    public class CreateModel : PageModel
    {
        private readonly Service_Auto.Data.Service_AutoContext _context;

        public CreateModel(Service_Auto.Data.Service_AutoContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var repairs = _context.Vehicle
                            .Include(v => v.Customer) // Ensure you include the Customer
                            .Select(v => new SelectListItem
                            {
                                Value = v.VehicleID.ToString(),
                                Text = $"{v.LicensePlate} - {v.Customer.Name}"
                            }).ToList();

            ViewData["VehicleID"] = new SelectList(repairs, "Value", "Text");

            return Page();
        }

        [BindProperty]
        public Repair Repair { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Repair.Add(Repair);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
