using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleAir.Domain.Model;

namespace SimpleAir.Domain.Repository.Builder
{
    public class AirportBuilder : IEntityTypeConfiguration<Airport>
    {
        public void Configure(EntityTypeBuilder<Airport> builder)
        {
            builder.HasKey(c => c.Id);
        }
    }
}