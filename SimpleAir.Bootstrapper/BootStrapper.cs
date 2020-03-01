using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleAir.Core;
using SimpleAir.Core.Repository;
using SimpleAir.Domain.Repository.Context;
using SimpleAir.Domain.Repository.Repository;
using SimpleAir.Domain.Service.Interface;
using SimpleAir.Domain.Service.Services;

namespace SimpleAir.Bootstrapper
{
    /// <summary>
    /// IOC injection manager
    /// </summary>
    public class BootStrapper
    {
        public void Register(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("ApplicationDbContext"));
            services.AddTransient<IFlightService, FlightService>();
            services.AddTransient<IFlightRepository, FlightRepository>();
            services.AddTransient<IAirportRepository, AirportRepository>();
            services.AddScoped<IApplicationDbContextResolver, ApplicationDbContextResolver>();

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
        }
    }
}