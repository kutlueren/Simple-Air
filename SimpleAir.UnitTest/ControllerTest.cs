using FluentAssertions;
using SimpleAir.Domain.Service.Interface;
using SimpleAir.Domain.Service.Model.Airport;
using SimpleAir.Domain.Service.Model.Flight;
using SimpleAir.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimpleAir.UnitTest
{
    public class ControllerTest : BaseTest
    {
        protected Mock<IFlightService> _flightService;

        public ControllerTest() : base()
        {

            _flightService = new Mock<IFlightService>();

            _flightService.Setup(t => t.GetAirportsAsync(It.IsAny<AirportRequestDto>())).Returns<AirportRequestDto>(async (obj) =>
            {
                var airports = await _airportRepository.Object.GetAirportsAsync(obj.SearchKey);

                List<AirportResponseDto> airportReponse = new List<AirportResponseDto>();

                _mapper.Map(airports, airportReponse);

                return airportReponse;
            });

            _flightService.Setup(t => t.GetAvailableFligthsAsync(It.IsAny<FlightRequestDto>())).Returns<FlightRequestDto>(async (obj) =>
            {
                var flights = await _flightRepository.Object.GetAvailableFligthsAsync(obj.DepartureId, obj.DestinationId, obj.StartDate);

                List<FlightResponseDto> airportResponse = new List<FlightResponseDto>();

                _mapper.Map(flights, airportResponse);

                return airportResponse;
            });
        }

        [Fact]
        public void Ctor_Tests()
        {
            Assert.Throws<ArgumentNullException>(() => { new FlightController(null); });
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
        public async Task HomeController_GetAirports_Should_Return_Convenient_AirportsAsync(string searchKey)
        {
            var controller = new FlightController(_flightService.Object);

            var response = await controller.GetAirports(new AirportRequestDto() { SearchKey = searchKey });

            var okResult = response as OkObjectResult;

            okResult.Should().NotBeNull();

            ICollection<AirportResponseDto> respValue = okResult.Value as ICollection<AirportResponseDto>;

            respValue.Count.Should().Be(airports.Where(t => t.Code.ToLower().Contains(searchKey.ToLower()) || t.Name.ToLower().Contains(searchKey.ToLower())).ToList().Count);
        }

        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public async Task HomeController_GetFlights_Should_Return_Convenient_FlightsAsync(int departure, int destination, DateTime startDate, DateTime endDate)
        {
            var controller = new FlightController(_flightService.Object);

            var response = await controller.GetFlights(new FlightRequestDto() { DepartureId = departure, DestinationId = destination, StartDate = startDate });

            var okResult = response as OkObjectResult;

            okResult.Should().NotBeNull();

            ICollection<FlightResponseDto> flightResponse = okResult.Value as ICollection<FlightResponseDto>;

            flightResponse.Count.Should().Be(flights.Where(t => t.Destination.Id == destination
            && t.Departure.Id == departure
            && t.Flightdate >= startDate && t.Flightdate <= endDate).ToList().Count);
        }
    }
}
