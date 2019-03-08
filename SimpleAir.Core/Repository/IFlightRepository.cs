using SimpleAir.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleAir.Core.Repository
{
    public interface IFlightRepository
    {
        Task<IEnumerable<Flight>> GetAvailableFligthsAsync(int departure, int destination, DateTime startDate, DateTime endDate);

        Task InsertFlightAsync(Flight flight);
    }
}
