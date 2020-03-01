using System;

namespace SimpleAir.Domain.Service.Model.Flight
{
    public class FlightRequestDto
    {
        /// <summary>
        /// Departure airport id
        /// </summary>
        public int DepartureId { get; set; }

        /// <summary>
        /// Destination airport id
        /// </summary>
        public int DestinationId { get; set; }

        /// <summary>
        /// Flight date
        /// </summary>
        public DateTime StartDate { get; set; }
    }
}