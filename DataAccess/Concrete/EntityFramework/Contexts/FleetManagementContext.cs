using DataAccess.Abstract.Repositories;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class FleetManagementContext: DbContext
    {
        public FleetManagementContext(DbContextOptions<FleetManagementContext> options) : base(options) { }
        public DbSet<Bag> Bags { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<WrongDeliveryLog> WrongDeliveryLogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bag>().ToTable(nameof(Bag), "Bag");
            modelBuilder.Entity<Package>().ToTable(nameof(Package), "Package");
            modelBuilder.Entity<Vehicle>().ToTable(nameof(Vehicle), "Vehicle");
            modelBuilder.Entity<WrongDeliveryLog>().ToTable(nameof(WrongDeliveryLog), "Log");
        }
    }
}
