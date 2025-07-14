using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingManagement.Domain.Entities;

namespace ParkingManagement.Infrastructure.Mapping
{
    public class PriceMapping : IEntityTypeConfiguration<Price>
    {
        public void Configure(EntityTypeBuilder<Price> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.BaseValue).IsRequired();
            builder.Property(x => x.ExtraTimeValue).IsRequired();
            builder.Property(x => x.EffectivePeriodStart).IsRequired();
            builder.Property(x => x.EffectivePeriodEnd);
            builder.Property(x => x.CreationDate).IsRequired();
        }
    }
}
