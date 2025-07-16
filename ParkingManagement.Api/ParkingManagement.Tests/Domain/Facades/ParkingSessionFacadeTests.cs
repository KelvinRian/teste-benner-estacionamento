using NSubstitute;
using ParkingManagement.Domain.Commands;
using ParkingManagement.Domain.Dtos;
using ParkingManagement.Domain.Entities;
using ParkingManagement.Domain.Facades;
using ParkingManagement.Domain.RepositoryInterfaces;
using ParkingManagement.Domain.Services;

namespace ParkingManagement.Tests.Domain.Facades
{
    public class ParkingSessionFacadeTests
    {
        private IParkingSessionRepository _parkingSessionRepository;
        private IPriceRepository _priceRepository;
        private IParkingSessionFacade _facade;
        private IPaymentCalculatorService _paymentCalculatorService;

        public ParkingSessionFacadeTests()
        {
            _parkingSessionRepository = Substitute.For<IParkingSessionRepository>();
            _priceRepository = Substitute.For<IPriceRepository>();
            _paymentCalculatorService = Substitute.For<IPaymentCalculatorService>();
            _facade = new ParkingSessionFacade(_parkingSessionRepository, _priceRepository, _paymentCalculatorService);
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

        [Fact]
        public async Task shouldGetAll()
        {
            // Arrange
            var command = new EntryCommand();
            var parkingSession = new ParkingSession(command, Guid.NewGuid());
            var parkingSession2 = new ParkingSession(command, Guid.NewGuid());

            _parkingSessionRepository.GetAll().Returns(new List<ParkingSession>() { parkingSession, parkingSession2 });

            var dto = new PaymentDto();
            var dto2 = new PaymentDto();

            _paymentCalculatorService.CalculatePayment(parkingSession).Returns(dto);
            _paymentCalculatorService.CalculatePayment(parkingSession2).Returns(dto2);

            // Act
            var result = await _facade.GetAll();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, x => x.Payment == dto);
            Assert.Contains(result, x => x.Payment == dto2);
        }
    }
}
