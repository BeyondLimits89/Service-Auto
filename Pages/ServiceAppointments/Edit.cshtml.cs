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

namespace Service_Auto.Pages.ServiceAppointments
{
    public class EditModel : PageModel
    {
        private readonly Service_Auto.Data.Service_AutoContext _context;

        public EditModel(Service_Auto.Data.Service_AutoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ServiceAppointment ServiceAppointment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statuses = Enum.GetValues(typeof(AppointmentStatus))
                       .Cast<AppointmentStatus>()
                       .Select(s => new SelectListItem
                       {
                           Value = ((int)s).ToString(),
                           Text = s.ToString()
                       });

            ViewData["Statuses"] = new SelectList(statuses, "Value", "Text");

            var vehicles = _context.Vehicle
            .Include(v => v.Customer) // Ensure you include the Customer
            .Select(v => new SelectListItem
            {
                Value = v.VehicleID.ToString(),
                Text = $"{v.LicensePlate} - {v.Customer.Name}"
            }).ToList();

            ViewData["VehicleID"] = new SelectList(vehicles, "Value", "Text");

            var serviceappointment =  await _context.ServiceAppointment.FirstOrDefaultAsync(m => m.AppointmentID == id);
            if (serviceappointment == null)
            {
                return NotFound();
            }
            ServiceAppointment = serviceappointment;


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

            _context.Attach(ServiceAppointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceAppointmentExists(ServiceAppointment.AppointmentID))
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

        private bool ServiceAppointmentExists(int id)
        {
            return _context.ServiceAppointment.Any(e => e.AppointmentID == id);
        }
    }
}
