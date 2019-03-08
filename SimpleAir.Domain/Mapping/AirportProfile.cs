using AutoMapper;
using SimpleAir.Domain.Service.Model.Airport;
using Entity = SimpleAir.Domain.Model;

namespace SimpleAir.Domain.Service.Mapping
{
    public class AirportProfile : Profile
    {
        public AirportProfile()
        {
            CreateMap<Entity.Airport, AirportResponseDto>()
                .ForMember(t => t.Code, x => x.MapFrom(a => a.Code))
                .ForMember(t => t.Id, x => x.MapFrom(a => a.Id))
                .ForMember(t => t.Name, x => x.MapFrom(a => a.Name));
        }
    }
}
