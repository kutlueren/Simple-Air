using System;

namespace SimpleAir.Domain.Model
{
    /// <summary>
    /// Flight Object
    /// </summary>
    public class Flight
    {
        /// <summary>
        /// Flight Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Fligth Fare to be charged
        /// </summary>
        public decimal Fare { get; set; }

        /// <summary>
        /// Fare Currency
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Departure Airport Id
        /// </summary>
        public int DepartureId { get; set; }

        /// <summary>
        /// Destination Airport Id
        /// </summary>
        public int DestinationId { get; set; }

        /// <summary>
        /// Destination Airport object
        /// </summary>
        public virtual Airport Destination { get; set; }

        /// <summary>
        /// Departure Airport object
        /// </summary>
        public virtual Airport Departure { get; set; }

        /// <summary>
        /// Flight Date
        /// </summary>
        public DateTime Flightdate { get; set; }

        /// <summary>
        /// Creates a flight with given parameters
        /// </summary>
        /// <param name="departure">Departure airport</param>
        /// <param name="destination">Destination airport</param>
        /// <param name="fare">Flight fare</param>
        /// <param name="currency">Fare currency</param>
        /// <param name="date">Flight date</param>
        /// <returns>New Flight object</returns>
        public static Flight Create(Airport departure, Airport destination, decimal fare, string currency, DateTime date)
        {
            //an event might be raised for event sourcing

            return new Flight() { DestinationId = destination.Id, DepartureId = departure.Id, Fare = fare, Departure = departure, Destination = destination, Currency = currency, Flightdate = date };
        }
    }
}