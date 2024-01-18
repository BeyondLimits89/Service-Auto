using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Service_Auto.Models;

namespace Service_Auto.Data
{
    public class Service_AutoContext : DbContext
    {
        public Service_AutoContext (DbContextOptions<Service_AutoContext> options)
            : base(options)
        {
        }

        public DbSet<Service_Auto.Models.Customer> Customer { get; set; } = default!;
        public DbSet<Service_Auto.Models.Repair> Repair { get; set; } = default!;
        public DbSet<Service_Auto.Models.ServiceAppointment> ServiceAppointment { get; set; } = default!;
        public DbSet<Service_Auto.Models.Vehicle> Vehicle { get; set; } = default!;
                
    }
    
}
