using AutoMapper;
using SimpleAir.Domain.Service.Model.Flight;
using Entity = SimpleAir.Domain.Model;

namespace SimpleAir.Domain.Service.Model.Mapping
{
    public class FlightProfile : Profile
    {
        public FlightProfile()
        {
            CreateMap<Entity.Flight, FlightResponseDto>()
                .ForMember(t => t.Date, x => x.MapFrom(a => a.Flightdate))
                .ForMember(t => t.DepartureAirportName, x => x.MapFrom(a => a.Departure.Name))
                .ForMember(t => t.DepartureCode, x => x.MapFrom(a => a.Departure.Code))
                .ForMember(t => t.DepartureId, x => x.MapFrom(a => a.Departure.Id))
                .ForMember(t => t.DestinationAirportName, x => x.MapFrom(a => a.Destination.Name))
                .ForMember(t => t.DestinationCode, x => x.MapFrom(a => a.Destination.Code))
                .ForMember(t => t.DestinationId, x => x.MapFrom(a => a.Destination.Id))
                .ForMember(t => t.Fare, x => x.MapFrom(a => a.Fare))
                .ForMember(t => t.Currency, x => x.MapFrom(a => a.Currency))
                .ForMember(t => t.FlightId, x => x.MapFrom(a => a.Id));
        }
    }
}