using SimpleAir.Core;
using SimpleAir.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading.Tasks;

namespace SimpleAir.Domain.Repository.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Flight>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Airport>().Property(p => p.Id).ValueGeneratedOnAdd();
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> AirPorts { get; set; }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await base.SaveChangesAsync();
        }
    }
}
