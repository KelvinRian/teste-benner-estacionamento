using Microsoft.EntityFrameworkCore;
using ParkingManagement.Domain.Entities;
using ParkingManagement.Infrastructure.Mapping;

namespace ParkingManagement.Infrastructure.Context
{
    public class ParkingManagementDbContext : DbContext
    {
        public ParkingManagementDbContext(DbContextOptions<ParkingManagementDbContext> options)
            : base(options)
        {
                
        }

        public DbSet<Price> Prices { get; set; }
        public DbSet<ParkingSession> ParkingSessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PriceMapping());
            modelBuilder.ApplyConfiguration(new ParkingSessionMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}
