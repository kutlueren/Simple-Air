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
    public class AirportRepository : IAirportRepository
    {
        private readonly IApplicationDbContextResolver _applicationDbContextResolver;
        private ApplicationDbContext _dbContext;

        public AirportRepository(IApplicationDbContextResolver applicationDbContextResolver)
        {
            _applicationDbContextResolver = applicationDbContextResolver ?? throw new ArgumentNullException(nameof(applicationDbContextResolver));

            _dbContext = _applicationDbContextResolver.GetCurrentDbContext<ApplicationDbContext>();
        }

        public async Task<IEnumerable<Airport>> GetAirportsAsync(string searchKey)
        {
            return await _dbContext.AirPorts.Where(t => t.Code.ToLower().Contains(searchKey.ToLower()) || t.Name.ToLower().Contains(searchKey.ToLower())).ToListAsync();
        }

        public async Task InsertAsync(Airport airport)
        {
            await _dbContext.AddAsync(airport);
        }
    }
}