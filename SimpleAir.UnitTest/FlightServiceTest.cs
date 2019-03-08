using FluentAssertions;
using SimpleAir.Domain.Service.Exception;
using SimpleAir.Domain.Service.Model.Airport;
using SimpleAir.Domain.Service.Model.Flight;
using SimpleAir.Domain.Service.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SimpleAir.UnitTest
{
    public class FlightServiceTest : BaseTest
    {
        public FlightServiceTest()
        {
        }

        [Fact]
        public void Ctor_Tests()
        {
            Assert.Throws<ArgumentNullException>(() => { new FlightService(null, null, null, null); });
            Assert.Throws<ArgumentNullException>(() => { new FlightService(_flightRepository.Object, null, null, null); });
            Assert.Throws<ArgumentNullException>(() => { new FlightService(_flightRepository.Object, _applicationDbContext.Object, null, null); });
            Assert.Throws<ArgumentNullException>(() => { new FlightService(_flightRepository.Object, _applicationDbContext.Object, _airportRepository.Object, null); });
        }

        [Theory]
        [InlineData("a")]
        [InlineData("fra")]
        [InlineData("lo")]
        [InlineData("zz")]
        [InlineData("da")]
        [InlineData("AMTD")]
        [InlineData("Amsterdam")]
        [InlineData("Fran")]
        public async Task FlightService_GetAirports_Should_Return_Convenient_AirportsAsync(string searchKey)
        {
            FlightService flightService = new FlightService(_flightRepository.Object, _applicationDbContext.Object, _airportRepository.Object, _mapper);

            ICollection<AirportResponseDto> airports = await flightService.GetAirportsAsync(new AirportRequestDto() { SearchKey = searchKey });

            airports.Count.Should().Be(airports.Where(t => t.Code.ToLower().Contains(searchKey.ToLower()) || t.Name.ToLower().Contains(searchKey.ToLower())).ToList().Count);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task FlightService_GetAirports_Should_Throw_Exception(string searchKey)
        {
            FlightService flightService = new FlightService(_flightRepository.Object, _applicationDbContext.Object, _airportRepository.Object, _mapper);

            await Assert.ThrowsAsync<BusinessException>(async () => { await flightService.GetAirportsAsync(new AirportRequestDto() { SearchKey = searchKey }); });
        }

        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public async Task FlightService_GetFlights_Should_Return_Convenient_FlightsAsync(int departure, int destination, DateTime startDate, DateTime endDate)
        {
            FlightService flightService = new FlightService(_flightRepository.Object, _applicationDbContext.Object, _airportRepository.Object, _mapper);

            ICollection<FlightResponseDto> flightResponse = await flightService.GetAvailableFligthsAsync(new FlightRequestDto() { DepartureId = departure, DestinationId = destination, EndDate = endDate, StartDate = startDate });

            flightResponse.Count.Should().Be(flights.Where(t => t.Destination.Id == destination
            && t.Departure.Id == departure
            && t.Flightdate >= startDate && t.Flightdate <= endDate).ToList().Count);
        }
    }
}
