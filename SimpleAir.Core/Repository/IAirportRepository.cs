using SimpleAir.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleAir.Core.Repository
{
    public interface IAirportRepository
    {
        Task InsertAsync(Airport airport);

        Task<IEnumerable<Airport>> GetAirportsAsync(string searchKey);
    }
}
