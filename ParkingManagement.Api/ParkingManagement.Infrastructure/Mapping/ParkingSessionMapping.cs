using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingManagement.Domain.Entities;

namespace ParkingManagement.Infrastructure.Mapping
{
    public class ParkingSessionMapping : IEntityTypeConfiguration<ParkingSession>
    {
        public void Configure(EntityTypeBuilder<ParkingSession> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.LicensePlate).IsRequired().HasMaxLength(8);
            builder.Property(x => x.EntryTime).IsRequired();
            builder.Property(x => x.ExitTime);
            builder.HasOne(x => x.Price)
                .WithMany()
                .HasForeignKey(x => x.PriceId);
        }
    }
}
