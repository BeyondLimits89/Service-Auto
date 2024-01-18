using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Service_Auto.Data;

namespace Service_Auto.API
{
    public class AppointmentsModel : PageModel
    {
        private readonly Service_AutoContext _context; 
        public AppointmentsModel(Service_AutoContext context)
        {
            _context = context;
        }

        public JsonResult OnGetJson(DateTime? start, DateTime? end)
        {
            var appointments = _context.ServiceAppointment
                .Where(a => (!start.HasValue || a.Date >= start.Value) && (!end.HasValue || a.Date <= end.Value))
                .Select(a => new
                {
                    title = $"{a.Time} - {a.Vehicle.LicensePlate} - {a.Vehicle.Customer.Name}",
                    start = new DateTime(a.Date.Year, a.Date.Month, a.Date.Day, a.Time.Hours, a.Time.Minutes, a.Time.Seconds)
                        .ToString("yyyy-MM-ddTHH:mm:ss"),
                    end = new DateTime(a.Date.Year, a.Date.Month, a.Date.Day, a.Time.Hours, a.Time.Minutes, a.Time.Seconds)
                        .AddMinutes(30).ToString("yyyy-MM-ddTHH:mm:ss"),
                    url = Url.Page("/ServiceAppointments/Edit", new { id = a.AppointmentID }),
                    extendedProps = new
                    {
                        customer = a.Vehicle.Customer.Name,
                        vehicle = $"{a.Vehicle.Make} - {a.Vehicle.Model} - {a.Vehicle.Year}"
                    }
                }).ToList();

            return new JsonResult(appointments);
        }




    }

}
