using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Service_Auto.Data;
using Service_Auto.Models;

namespace Service_Auto.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly Service_Auto.Data.Service_AutoContext _context;
        private readonly int pageSize = 15;
        public PaginatedList<Customer> Customer { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public string CurrentSort { get; set; }
        public string NameSort { get; set; }
        public string CurrentFilter { get; set; }
        public IndexModel(Service_Auto.Data.Service_AutoContext context)
        {
            _context = context;
        }       

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int currentPage = 1)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            CurrentFilter = searchString;

            if (searchString != null)
            {
                currentPage = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            IQueryable<Customer> customerIQ = from s in _context.Customer select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                customerIQ = customerIQ.Where(s => s.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    customerIQ = customerIQ.OrderByDescending(s => s.Name);
                    break;
                default:
                    customerIQ = customerIQ.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 20;
            Customer = await PaginatedList<Customer>.CreateAsync(customerIQ.AsNoTracking(), currentPage, pageSize);
        }


    }
}
