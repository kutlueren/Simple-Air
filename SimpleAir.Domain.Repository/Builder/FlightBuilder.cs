using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleAir.Domain.Model;

namespace SimpleAir.Domain.Repository.Builder
{
    public class FlightBuilder : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasOne(c => c.Departure);
            builder.HasOne(c => c.Destination);
        }
    }
}