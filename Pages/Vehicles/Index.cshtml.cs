using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Service_Auto.Data;
using Service_Auto.Models;

namespace Service_Auto.Pages.Vehicles
{
   
    public class IndexModel : PageModel
    {
        private readonly Service_Auto.Data.Service_AutoContext _context;
        private readonly int pageSize = 15;
        public PaginatedList<Vehicle> Vehicle { get; set; }
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

            IQueryable<Vehicle> vehicleIQ = from s in _context.Vehicle.Include(v => v.Customer)
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
                vehicleIQ = vehicleIQ.Where(s => s.Customer.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "description_desc":
                    vehicleIQ = vehicleIQ.OrderByDescending(s => s.Customer.Name);
                    break;
                default:
                    vehicleIQ = vehicleIQ.OrderBy(s => s.Customer.Name);
                    break;
            }

            int totalRepairs = await vehicleIQ.CountAsync();
            TotalPages = (int)Math.Ceiling(totalRepairs / (double)pageSize);
            CurrentPage = currentPage;

            Vehicle = await PaginatedList<Vehicle>.CreateAsync(vehicleIQ, currentPage, pageSize);
        }
    }
}
