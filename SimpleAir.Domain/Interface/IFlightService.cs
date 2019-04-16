using SimpleAir.Domain.Service.Model.Airport;
using SimpleAir.Domain.Service.Model.Flight;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleAir.Domain.Service.Interface
{
    /// <summary>
    /// Business service to search, save flights and search airports
    /// </summary>
    public interface IFlightService
    {
        /// <summary>
        /// Returns available flights with given parameters
        /// </summary>
        /// <param name="request">FlightRequestDto object containing departure and destination ids, flight date</param>
        /// <returns></returns>
        Task<ICollection<FlightResponseDto>> GetAvailableFligthsAsync(FlightRequestDto request);

        /// <summary>
        /// Inserts dummy flight data 
        /// </summary>
        /// <returns></returns>
        Task GenerateDummyFlightDataAsync();

        /// <summary>
        /// Gets airports with given parameters
        /// </summary>
        /// <param name="request">AirportRequestDto object containing a search key</param>
        /// <returns></returns>
        Task<ICollection<AirportResponseDto>> GetAirportsAsync(AirportRequestDto request);
    }
}
