using Microsoft.EntityFrameworkCore;
using SimpleAir.Core;
using SimpleAir.Core.Repository;
using SimpleAir.Domain.Model;
using SimpleAir.Domain.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAir.Domain.Repository.Repository
{
    public class FlightRepository : IFlightRepository
    {
        private readonly IApplicationDbContextResolver _applicationDbContextResolver;
        private ApplicationDbContext _dbContext;

        public FlightRepository(IApplicationDbContextResolver applicationDbContextResolver)
        {
            _applicationDbContextResolver = applicationDbContextResolver ?? throw new ArgumentNullException(nameof(applicationDbContextResolver));

            _dbContext = _applicationDbContextResolver.GetCurrentDbContext<ApplicationDbContext>();
        }

        public async Task InsertFlightAsync(Flight flight)
        {
            await _dbContext.Flights.AddAsync(flight);
        }

        public async Task<IEnumerable<Flight>> GetAvailableFligthsAsync(int departure, int destination, DateTime startDate)
        {
            var flights = await _dbContext.Flights.Include(u => u.Destination).Include(u => u.Departure).Where(t => t.Destination.Id == destination
            && t.Departure.Id == departure
            && t.Flightdate >= startDate).ToListAsync();

            return flights;
        }
    }
}