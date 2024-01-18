using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Service_Auto.Data;
using Service_Auto.Models;

namespace Service_Auto.Pages.ServiceAppointments
{
    public class DetailsModel : PageModel
    {
        private readonly Service_Auto.Data.Service_AutoContext _context;

        public DetailsModel(Service_Auto.Data.Service_AutoContext context)
        {
            _context = context;
        }

        public ServiceAppointment ServiceAppointment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            } 

            var serviceappointment = await _context.ServiceAppointment
                                    .Include(s => s.Vehicle).ThenInclude(v => v.Customer)
                                    .FirstOrDefaultAsync(m => m.AppointmentID == id);
            if (serviceappointment == null)
            {
                return NotFound();
            }
            else
            {
                ServiceAppointment = serviceappointment;
            }
            return Page();
        }
    }
}
