using NSubstitute;
using ParkingManagement.Domain.Commands;
using ParkingManagement.Domain.Entities;
using ParkingManagement.Domain.Facades;
using ParkingManagement.Domain.RepositoryInterfaces;

namespace ParkingManagement.Tests.Domain.Facades
{
    public class ParkingSessionFacadeTests
    {
        private IParkingSessionRepository _parkingSessionRepository;
        private IPriceRepository _priceRepository;
        private IParkingSessionFacade _facade;

        public ParkingSessionFacadeTests()
        {
            _parkingSessionRepository = Substitute.For<IParkingSessionRepository>();
            _priceRepository = Substitute.For<IPriceRepository>();
            _facade = new ParkingSessionFacade(_parkingSessionRepository, _priceRepository);
        }

        [Fact]
        public async Task shouldEntry()
        {
            // Arrange
            var price = new Price(new CreatePriceCommand());
            _priceRepository.GetLatestApplicableFor(Arg.Any<DateTime>()).Returns(price);

            var command = new EntryCommand()
            {
                LicensePlate = "aaa-1234"
            };

            // Act
            await _facade.Entry(command);

            // Assert
            await _parkingSessionRepository
                .Received(1)
                .Create(Arg.Any<ParkingSession>());
        }

        [Fact]
        public async Task shouldExit()
        {
            // Arrange
            var command = new EntryCommand()
            {
                LicensePlate = "aaa-1234"
            };
            var parkingSession = new ParkingSession(command, Guid.NewGuid());
            var id = Guid.NewGuid();

            _parkingSessionRepository.GetById(id).Returns(parkingSession);

            // Act
            await _facade.Exit(id);

            // Assert
            await _parkingSessionRepository
                .Received(1)
                .Update(parkingSession);

            Assert.NotNull(parkingSession.ExitTime);
        }
    }
}
