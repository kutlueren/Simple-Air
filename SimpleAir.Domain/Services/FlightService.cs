using AutoMapper;
using SimpleAir.Core;
using SimpleAir.Core.Repository;
using SimpleAir.Domain.Model;
using SimpleAir.Domain.Service.Exception;
using SimpleAir.Domain.Service.Interface;
using SimpleAir.Domain.Service.Model.Airport;
using SimpleAir.Domain.Service.Model.Flight;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleAir.Domain.Service.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IAirportRepository _airportRepository;
        private readonly IMapper _mapper;

        public FlightService(IFlightRepository flightRepository, IApplicationDbContext applicationDbContext, IAirportRepository airportRepository, IMapper mapper)
        {
            _flightRepository = flightRepository ?? throw new ArgumentNullException(nameof(flightRepository));
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _airportRepository = airportRepository ?? throw new ArgumentNullException(nameof(airportRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task GenerateDummyFlightDataAsync()
        {
            Airport airport1 = Airport.Create("Amsterdam", "SCPL");
            Airport airport2 = Airport.Create("London", "LON");
            Airport airport3 = Airport.Create("Frankfurt", "FRK");

            await _airportRepository.InsertAsync(airport1);
            await _airportRepository.InsertAsync(airport2);
            await _airportRepository.InsertAsync(airport3);

            Flight flight1 = Flight.Create(airport1, airport2, 150, "EUR", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(3).Day, 9, 55, 00));
            Flight flight2 = Flight.Create(airport2, airport1, 150, "EUR", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(4).Day, 13, 15, 00));
            Flight flight3 = Flight.Create(airport1, airport3, 170, "EUR", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(5).Day, 10, 45, 00));
            Flight flight4 = Flight.Create(airport3, airport2, 200, "EUR", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(2).Day, 14, 35, 00));

            await _flightRepository.InsertFlightAsync(flight1);
            await _flightRepository.InsertFlightAsync(flight2);
            await _flightRepository.InsertFlightAsync(flight3);
            await _flightRepository.InsertFlightAsync(flight4);

            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<ICollection<FlightResponseDto>> GetAvailableFligthsAsync(FlightRequestDto request)
        {
            var flights = await _flightRepository.GetAvailableFligthsAsync(request.DepartureId, request.DestinationId, request.StartDate);

            List<FlightResponseDto> flightResponseList = new List<FlightResponseDto>();

            _mapper.Map(flights, flightResponseList);

            return flightResponseList;
        }

        public async Task<ICollection<AirportResponseDto>> GetAirportsAsync(AirportRequestDto request)
        {
            if (request == null || string.IsNullOrEmpty(request.SearchKey))
            {
                throw new BusinessException("request or search key null.");
            }

            var airports = await _airportRepository.GetAirportsAsync(request.SearchKey);

            List<AirportResponseDto> airportResponseList = new List<AirportResponseDto>();

            _mapper.Map(airports, airportResponseList);

            return airportResponseList;
        }
    }
}