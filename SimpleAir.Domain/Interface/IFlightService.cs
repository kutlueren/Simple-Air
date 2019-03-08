using SimpleAir.Domain.Service.Model.Airport;
using SimpleAir.Domain.Service.Model.Flight;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleAir.Domain.Service.Interface
{
    public interface IFlightService
    {
        Task<ICollection<FlightResponseDto>> GetAvailableFligthsAsync(FlightRequestDto request);
        Task GenerateDummyFlightDataAsync();
        Task<ICollection<AirportResponseDto>> GetAirportsAsync(AirportRequestDto request);
    }
}
