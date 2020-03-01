using System;

namespace SimpleAir.Domain.Service.Model.Flight
{
    public class FlightResponseDto
    {
        public int DepartureId { get; set; }
        public string DepartureAirportName { get; set; }
        public string DepartureCode { get; set; }
        public int DestinationId { get; set; }
        public string DestinationAirportName { get; set; }
        public string DestinationCode { get; set; }
        public DateTime Date { get; set; }
        public decimal Fare { get; set; }
        public string Currency { get; set; }
        public int FlightId { get; set; }
    }
}