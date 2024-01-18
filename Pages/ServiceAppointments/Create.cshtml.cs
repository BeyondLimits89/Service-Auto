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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Service_Auto.Pages.ServiceAppointments
{
    public class CreateModel : PageModel
    {
        private readonly Service_Auto.Data.Service_AutoContext _context;

        public DateTime? Date { get; set; }

        public CreateModel(Service_Auto.Data.Service_AutoContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(DateTime? date)
        {
            var vehicles = _context.Vehicle
                        .Include(v => v.Customer) // Ensure you include the Customer
                        .Select(v => new SelectListItem
                        {
                            Value = v.VehicleID.ToString(),
                            Text = $"{v.LicensePlate} - {v.Customer.Name}"
                        }).ToList();

            ViewData["VehicleID"] = new SelectList(vehicles, "Value", "Text");

            var statuses = Enum.GetValues(typeof(AppointmentStatus))
                       .Cast<AppointmentStatus>()
                       .Select(s => new SelectListItem
                       {
                           Value = ((int)s).ToString(),
                           Text = s.ToString()
                       });

            ViewData["Statuses"] = new SelectList(statuses, "Value", "Text");


            var timeOptions = new List<SelectListItem>();

            for (int hour = 7; hour <= 20; hour++)
            {
                timeOptions.Add(new SelectListItem { Value = $"{hour}:00", Text = $"{hour}:00" });
                timeOptions.Add(new SelectListItem { Value = $"{hour}:30", Text = $"{hour}:30" });
            }

            ViewData["TimeOptions"] = new SelectList(timeOptions, "Value", "Text");

            return Page();
         
        }

        [BindProperty]
        public ServiceAppointment ServiceAppointment { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ServiceAppointment.Add(ServiceAppointment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
