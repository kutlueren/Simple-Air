using SimpleAir.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleAir.Core.Repository
{
    /// <summary>
    /// Airport repository for airport db transactions
    /// </summary>
    public interface IAirportRepository
    {
        /// <summary>
        /// Inserts new airport async
        /// </summary>
        /// <param name="airport"></param>
        /// <returns></returns>
        Task InsertAsync(Airport airport);

        /// <summary>
        /// Gets airports aligning with search key
        /// </summary>
        /// <param name="searchKey">Key to search airport. </param>
        /// <returns>Airport list thats contains or equals to search key</returns>
        Task<IEnumerable<Airport>> GetAirportsAsync(string searchKey);
    }
}