using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Service_Auto.Data;
using Service_Auto.Models;

namespace Service_Auto.Pages.ServiceAppointments
{    
    public class IndexModel : PageModel
    {
        private readonly Service_Auto.Data.Service_AutoContext _context;
        private readonly int pageSize = 15;
        public PaginatedList<ServiceAppointment> ServiceAppointment { get; set; }
        public int TotalPages { get; set; }
        public string CurrentSort { get; set; }
        public string DescriptionSort { get; set; }
        public string CurrentFilter { get; set; }
        public int CurrentPage { get; set; } = 1;

        public IndexModel(Service_Auto.Data.Service_AutoContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int currentPage = 1)
        {
            CurrentSort = sortOrder;
            DescriptionSort = sortOrder == "Description" ? "description_desc" : "Description";
            CurrentFilter = searchString;

            IQueryable<ServiceAppointment> serviceAppointmentIQ = from s in _context.ServiceAppointment.Include(r => r.Vehicle).ThenInclude(v => v.Customer)
                                          select s;

            if (searchString != null)
            {
                currentPage = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                serviceAppointmentIQ = serviceAppointmentIQ.Where(s => s.Vehicle.Customer.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "description_desc":
                    serviceAppointmentIQ = serviceAppointmentIQ.OrderByDescending(s => s.Vehicle.Customer.Name);
                    break;
                default:
                    serviceAppointmentIQ = serviceAppointmentIQ.OrderBy(s => s.Vehicle.Customer.Name);
                    break;
            }

            int totalServiceAppointments = await serviceAppointmentIQ.CountAsync();
            TotalPages = (int)Math.Ceiling(totalServiceAppointments / (double)pageSize);
            CurrentPage = currentPage;

            ServiceAppointment = await PaginatedList<ServiceAppointment>.CreateAsync(serviceAppointmentIQ, currentPage, pageSize);
        }
    }
}
