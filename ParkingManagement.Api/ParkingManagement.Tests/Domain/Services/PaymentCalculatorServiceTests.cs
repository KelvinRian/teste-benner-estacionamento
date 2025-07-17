using ParkingManagement.Domain.Commands;
using ParkingManagement.Domain.Entities;
using ParkingManagement.Domain.Services;

namespace ParkingManagement.Tests.Domain.Services
{
    public class PaymentCalculatorServiceTests
    {
        private IPaymentCalculatorService _service;

        public PaymentCalculatorServiceTests()
        {

            _service = new PaymentCalculatorService();
        }

        [Fact]
        public void shouldCalculatePaymentForOneHourSession()
        {
            // Arrange
            var createPriceCommand = new CreatePriceCommand()
            {
                BaseValue = 5,
                ExtraTimeValue = 3
            };

            var price = new Price(createPriceCommand);
            var parkingSession = new ParkingSession(new EntryCommand(), Guid.NewGuid());

            typeof(ParkingSession).GetProperty("ExitTime").SetValue(parkingSession, parkingSession.EntryTime.AddHours(1));
            typeof(ParkingSession).GetProperty("Price").SetValue(parkingSession, price);
            typeof(ParkingSession).GetProperty("Finished").SetValue(parkingSession, true);

            // Act
            var result = _service.CalculatePayment(parkingSession);

            // Arrange
            Assert.NotNull(result);

            var expectedDuration = parkingSession.ExitTime - parkingSession.EntryTime;
            Assert.Equal(expectedDuration, result.Duration);

            Assert.Equal(1, result.NumberOfHoursToPay);

            Assert.Equal(price.BaseValue, result.PriceBaseValue);

            var expectedTotalPayable = price.BaseValue;
            Assert.Equal(expectedTotalPayable, result.TotalPayable);
        }

        [Theory]
        [InlineData(30)]
        [InlineData(29)]
        public void shouldCalculatePaymentForHalfAnHourSession(int minutes)
        {
            // Arrange
            var createPriceCommand = new CreatePriceCommand()
            {
                BaseValue = 5,
                ExtraTimeValue = 3
            };

            var price = new Price(createPriceCommand);

            var parkingSession = new ParkingSession(new EntryCommand(), Guid.NewGuid());

            typeof(ParkingSession).GetProperty("ExitTime").SetValue(parkingSession, parkingSession.EntryTime.AddMinutes(minutes));
            typeof(ParkingSession).GetProperty("Price").SetValue(parkingSession, price);
            typeof(ParkingSession).GetProperty("Finished").SetValue(parkingSession, true);

            // Act
            var result = _service.CalculatePayment(parkingSession);

            // Arrange
            Assert.NotNull(result);

            var expectedDuration = parkingSession.ExitTime - parkingSession.EntryTime;
            Assert.Equal(expectedDuration, result.Duration);

            Assert.Equal(0.5, result.NumberOfHoursToPay);

            Assert.Equal(price.BaseValue, result.PriceBaseValue);

            var expectedTotalPayable = price.BaseValue / 2;
            Assert.Equal(expectedTotalPayable, result.TotalPayable);
        }

        [Theory]
        [InlineData(1, 5)]
        [InlineData(2, 8)]
        public void shouldCalculateExtraTime(int hours, decimal expectedTotalPayable)
        {
            // Arrange
            var createPriceCommand = new CreatePriceCommand()
            {
                BaseValue = 5,
                ExtraTimeValue = 3
            };

            var price = new Price(createPriceCommand);

            var parkingSession = new ParkingSession(new EntryCommand(), Guid.NewGuid());

            typeof(ParkingSession).GetProperty("ExitTime").SetValue(parkingSession, parkingSession.EntryTime.AddHours(hours).AddMinutes(11));
            typeof(ParkingSession).GetProperty("Price").SetValue(parkingSession, price);
            typeof(ParkingSession).GetProperty("Finished").SetValue(parkingSession, true);

            // Act
            var result = _service.CalculatePayment(parkingSession);

            // Arrange
            Assert.NotNull(result);

            var expectedDuration = parkingSession.ExitTime - parkingSession.EntryTime;
            Assert.Equal(expectedDuration, result.Duration);

            var expectedNumberOfHoursToPay = hours++;
            Assert.Equal(expectedNumberOfHoursToPay, result.NumberOfHoursToPay);

            Assert.Equal(price.BaseValue, result.PriceBaseValue);

            Assert.Equal(expectedTotalPayable, result.TotalPayable);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(9)]
        public void shouldCalculateOneHourWithTolerance(int minutes)
        {
            // Arrange
            var createPriceCommand = new CreatePriceCommand()
            {
                BaseValue = 5,
                ExtraTimeValue = 3
            };

            var price = new Price(createPriceCommand);

            var parkingSession = new ParkingSession(new EntryCommand(), Guid.NewGuid());

            typeof(ParkingSession).GetProperty("ExitTime").SetValue(parkingSession, parkingSession.EntryTime.AddHours(1).AddMinutes(minutes));
            typeof(ParkingSession).GetProperty("Price").SetValue(parkingSession, price);
            typeof(ParkingSession).GetProperty("Finished").SetValue(parkingSession, true);

            // Act
            var result = _service.CalculatePayment(parkingSession);

            // Arrange
            Assert.NotNull(result);

            var expectedDuration = parkingSession.ExitTime - parkingSession.EntryTime;
            Assert.Equal(expectedDuration, result.Duration);

            Assert.Equal(1, result.NumberOfHoursToPay);

            Assert.Equal(price.BaseValue, result.PriceBaseValue);

            var expectedTotalPayable = price.BaseValue;
            Assert.Equal(expectedTotalPayable, result.TotalPayable);
        }
    }
}
