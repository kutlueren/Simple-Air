using System;

namespace SimpleAir.Domain.Service.Model.Flight
{
    public class FlightRequestDto
    {
        public int DepartureId { get; set; }
        public int DestinationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
