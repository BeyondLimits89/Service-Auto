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
    public class IndexModel : PageModel
    {
        private readonly Service_Auto.Data.Service_AutoContext _context;
        private readonly int pageSize = 15;
        public PaginatedList<Repair> Repair { get; set; }
        public string CurrentSort { get; set; }
        public string DescriptionSort { get; set; }
        public string CurrentFilter { get; set; }
        public int TotalPages { get; set; }
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

            if (searchString != null)
            {
                currentPage = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            IQueryable<Repair> repairIQ = from s in _context.Repair.Include(r => r.Vehicle).ThenInclude(v => v.Customer)
                                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                repairIQ = repairIQ.Where(s => s.Description.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "description_desc":
                    repairIQ = repairIQ.OrderByDescending(s => s.Description);
                    break;
                default:
                    repairIQ = repairIQ.OrderBy(s => s.Description);
                    break;
            }

            int totalRepairs = await repairIQ.CountAsync();
            TotalPages = (int)Math.Ceiling(totalRepairs / (double)pageSize);
            CurrentPage = currentPage;

            Repair = await PaginatedList<Repair>.CreateAsync(repairIQ, currentPage, pageSize);
        }
    }


}
