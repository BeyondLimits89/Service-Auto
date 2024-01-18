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
    public class DeleteModel : PageModel
    {
        private readonly Service_Auto.Data.Service_AutoContext _context;

        public DeleteModel(Service_Auto.Data.Service_AutoContext context)
        {
            _context = context;
        }

        public string VehicleDetails { get; set; }

        [BindProperty]
        public ServiceAppointment ServiceAppointment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ServiceAppointment = await _context.ServiceAppointment
                                     .Include(s => s.Vehicle).ThenInclude(v => v.Customer)
                                     .FirstOrDefaultAsync(m => m.AppointmentID == id);

            if (ServiceAppointment == null)
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

            var serviceappointment = await _context.ServiceAppointment.FindAsync(id);
            if (serviceappointment != null)
            {
                ServiceAppointment = serviceappointment;
                _context.ServiceAppointment.Remove(ServiceAppointment);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
