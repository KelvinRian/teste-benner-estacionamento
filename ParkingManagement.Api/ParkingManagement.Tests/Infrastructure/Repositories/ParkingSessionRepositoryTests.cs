using EntityFrameworkCore.Testing.Moq;
using Microsoft.EntityFrameworkCore;
using ParkingManagement.Domain.Commands;
using ParkingManagement.Domain.Entities;
using ParkingManagement.Infrastructure.Context;
using ParkingManagement.Infrastructure.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ParkingManagement.Tests.Infrastructure.Repositories
{
    public class ParkingSessionRepositoryTests
    {
        [Fact]
        public async Task shouldCreate()
        {
            // Arrange
            var mockContext = Create.MockedDbContextFor<ParkingManagementDbContext>();

            var repository = new ParkingSessionRepository(mockContext);

            var command = new EntryCommand()
            {
                LicensePlate = "aaa-1234"
            };
            var parkingSession = CreateParkingSession(command);

            // Act
            await repository.Create(parkingSession);

            // Assert
            var createdParkingSession = mockContext.ParkingSessions.Local.FirstOrDefault();
            Assert.NotNull(createdParkingSession);
            Assert.Equal(parkingSession, createdParkingSession);
        }

        private ParkingSession CreateParkingSession(EntryCommand command)
        {
            var priceId = Guid.NewGuid();
            return new ParkingSession(command, priceId);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllSessions()
        {
            // Arrange
            var mockContext = Create.MockedDbContextFor<ParkingManagementDbContext>();

            var oldestSession = CreateParkingSession(new EntryCommand()
            {
                LicensePlate = "aaa-1234"
            });

            var newestSession = CreateParkingSession(new EntryCommand()
            {
                LicensePlate = "bbb-5678"
            });

            await mockContext.ParkingSessions.AddRangeAsync(oldestSession, newestSession);
            await mockContext.SaveChangesAsync();

            var repository = new ParkingSessionRepository(mockContext);

            // Act
            var result = await repository.GetAll();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal(newestSession, result.First());
        }

        [Fact]
        public async Task shouldUpdate()
        {
            // Arrange
            var mockContext = Create.MockedDbContextFor<ParkingManagementDbContext>();

            var repository = new ParkingSessionRepository(mockContext);

            var command = new EntryCommand()
            {
                LicensePlate = "aaa-1234"
            };
            var parkingSession = CreateParkingSession(command);
            await mockContext.ParkingSessions.AddAsync(parkingSession);
            await mockContext.SaveChangesAsync();

            // Act
            parkingSession.Exit();
            await repository.Update(parkingSession);

            // Assert
            mockContext.Entry(parkingSession).State = EntityState.Detached;
            var result = await mockContext.ParkingSessions.FindAsync(parkingSession.Id);

            Assert.Equal(parkingSession.Id, result?.Id);
            Assert.NotNull(parkingSession.ExitTime);
        }
    }
}
