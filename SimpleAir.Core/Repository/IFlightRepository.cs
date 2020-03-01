using SimpleAir.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleAir.Core.Repository
{
    /// <summary>
    /// Flight repository for flight db transactions
    /// </summary>
    public interface IFlightRepository
    {
        /// <summary>
        /// Gets flights aligning with given parameters
        /// </summary>
        /// <param name="departure">Departure airport id</param>
        /// <param name="destination">Destination airport id</param>
        /// <param name="startDate">Flight start date</param>
        /// <returns>Flights equals to departure and destination, equal or greater than selected date </returns>
        Task<IEnumerable<Flight>> GetAvailableFligthsAsync(int departure, int destination, DateTime startDate);

        /// <summary>
        /// Inserts new flight to db
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        Task InsertFlightAsync(Flight flight);
    }
}