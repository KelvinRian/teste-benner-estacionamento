using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ParkingManagement.Infrastructure.Context;

public class ParkingManagementDbContextFactory : IDesignTimeDbContextFactory<ParkingManagementDbContext>
{
    public ParkingManagementDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ParkingManagementDbContext>();

        optionsBuilder.UseSqlServer("Server=Kelvin;Database=ParkingManagement;User Id=sa;Password=12345678;Trusted_Connection=True;TrustServerCertificate=True;");

        return new ParkingManagementDbContext(optionsBuilder.Options);
    }
}
