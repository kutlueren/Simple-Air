using System;

namespace SimpleAir.Domain.Model
{
    public class Flight
    {
        public int Id { get; set; }
        public decimal Fare { get; set; }
        public string Currency { get; set; }

        public int DepartureId { get; set; }

        public int DestinationId { get; set; }

        public virtual Airport Destination { get; set; }

        public virtual Airport Departure { get; set; }

        public DateTime Flightdate { get; set; }

        public static Flight Create(Airport departure, Airport destination, decimal fare, string currency, DateTime date)
        {
            //an event might be raised for event sourcing

            return new Flight() { DestinationId = destination.Id, DepartureId = departure.Id, Fare = fare, Departure = departure, Destination = destination, Currency = currency, Flightdate = date };
        }
    }
}
