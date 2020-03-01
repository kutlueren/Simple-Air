using AutoMapper;
using Moq;
using SimpleAir.Core;
using SimpleAir.Core.Repository;
using SimpleAir.Domain.Model;
using SimpleAir.Domain.Service.Mapping;
using SimpleAir.Domain.Service.Model.Mapping;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAir.UnitTest
{
    public class BaseTest
    {
        protected Mock<IFlightRepository> _flightRepository;
        protected Mock<IApplicationDbContext> _applicationDbContext;
        protected Mock<IAirportRepository> _airportRepository;
        protected IMapper _mapper;
        protected ICollection<Airport> airports;
        protected ICollection<Flight> flights;

        public BaseTest()
        {
            _flightRepository = new Mock<IFlightRepository>();
            _applicationDbContext = new Mock<IApplicationDbContext>();
            _airportRepository = new Mock<IAirportRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FlightProfile>();
                cfg.AddProfile<AirportProfile>();
            });

            _mapper = config.CreateMapper();

            SetRepositories();
        }

        private void SetRepositories()
        {
            Airport airport1 = Airport.Create("Amsterdam", "SCPL");
            Airport airport2 = Airport.Create("London", "LON");
            Airport airport3 = Airport.Create("Frankfurt", "FRK");

            airport1.Id = 1;
            airport1.Id = 2;
            airport1.Id = 3;

            airports = new List<Airport>();

            airports.Add(airport1);
            airports.Add(airport2);
            airports.Add(airport2);

            _airportRepository.Setup(t => t.GetAirportsAsync(It.IsAny<string>())).Returns<string>(async (key) =>
            {
                return await Task.FromResult<ICollection<Airport>>(airports.Where(t => t.Code.ToLower().Contains(key.ToLower()) || t.Name.ToLower().Contains(key.ToLower())).ToList());
            });

            Flight flight1 = Flight.Create(airport1, airport2, 150, "EUR", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(3).Day, 9, 55, 00));
            Flight flight2 = Flight.Create(airport2, airport1, 150, "EUR", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(4).Day, 13, 15, 00));
            Flight flight3 = Flight.Create(airport1, airport3, 170, "EUR", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(5).Day, 10, 45, 00));
            Flight flight4 = Flight.Create(airport3, airport2, 200, "EUR", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(2).Day, 14, 35, 00));

            flight1.Id = 1;
            flight2.Id = 2;
            flight3.Id = 3;
            flight4.Id = 4;

            flights = new List<Flight>();

            flights.Add(flight1);
            flights.Add(flight2);
            flights.Add(flight3);
            flights.Add(flight4);

            _flightRepository.Setup(t => t.GetAvailableFligthsAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>())).Returns<int, int, DateTime>(async (int departure, int destination, DateTime startDate) =>
            {
                return await Task.FromResult<ICollection<Flight>>(flights.Where(t => t.Destination.Id == destination
                && t.Departure.Id == departure
                && t.Flightdate >= startDate).ToList());
            });
        }

        protected class TestDataGenerator : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
            {
                new object[] { 1, 2, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(3).Day, 9, 55, 00), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(5).Day)},
                new object[] { 2, 1, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day, 9, 55, 00), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(5).Day)},
                new object[] { 3, 2, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(2).Day, 9, 55, 00), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(5).Day)},
                new object[] { 3, 1, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(6).Day, 9, 55, 00), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(9).Day)}
            };

            public IEnumerator<object[]> GetEnumerator()
            {
                return _data.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return _data.GetEnumerator();
            }
        }
    }
}