using ParkingManagement.Domain.Commands;
using ParkingManagement.Domain.Entities;

namespace ParkingManagement.Tests.Domain.Entities
{
    public class ParkingSessionTests
    {
        [Fact]
        public void shouldConstruct()
        {
            var command = new EntryCommand()
            {
                LicensePlate = "ABC-1234"
            };

            var priceId = Guid.NewGuid();

            var result = new ParkingSession(command, priceId);

            Assert.Equal(command.LicensePlate, result.LicensePlate);
            Assert.InRange(result.EntryTime, DateTime.UtcNow.AddSeconds(-59), DateTime.UtcNow.AddSeconds(59));
            Assert.Equal(DateTimeKind.Utc, result.EntryTime.Kind);
            Assert.Null(result.ExitTime);
            Assert.Equal(priceId, result.PriceId);
            Assert.Null(result.Price);
        }

        [Fact]
        public void shouldExit()
        {
            var command = new EntryCommand();
            var priceId = Guid.NewGuid();

            var parkingSession = new ParkingSession(command, priceId);

            parkingSession.Exit();

            Assert.NotNull(parkingSession.ExitTime);
            Assert.InRange(parkingSession.ExitTime.Value, DateTime.UtcNow.AddSeconds(-59), DateTime.UtcNow.AddSeconds(59));
            Assert.Equal(DateTimeKind.Utc, parkingSession.ExitTime.Value.Kind);
        }

        [Fact]
        public void shouldGetBaseValue()
        {
            var expectedBaseValue = 10;
            var priceCommand = new CreatePriceCommand()
            {
                BaseValue = expectedBaseValue,
            };

            var price = new Price(priceCommand);

            var entryCommand = new EntryCommand();
            var parkingSessionCommand = new ParkingSession(entryCommand, Guid.NewGuid());
            typeof(ParkingSession).GetProperty("Price").SetValue(parkingSessionCommand, price);

            var result = parkingSessionCommand.GetBaseValue();

            Assert.Equal(expectedBaseValue, result);
        }

        [Fact]
        public void shouldGetBaseValueFromNullPrice()
        {
            var entryCommand = new EntryCommand();
            var parkingSessionCommand = new ParkingSession(entryCommand, Guid.NewGuid());

            var result = parkingSessionCommand.GetBaseValue();

            Assert.Equal(0, result);
        }

        [Fact]
        public void shouldGetExtraTimeValue()
        {
            var expectedExtraTimeValue = 11;
            var priceCommand = new CreatePriceCommand()
            {
                ExtraTimeValue = expectedExtraTimeValue,
            };

            var price = new Price(priceCommand);

            var entryCommand = new EntryCommand();
            var parkingSessionCommand = new ParkingSession(entryCommand, Guid.NewGuid());
            typeof(ParkingSession).GetProperty("Price").SetValue(parkingSessionCommand, price);

            var result = parkingSessionCommand.GetExtraTimeValue();

            Assert.Equal(expectedExtraTimeValue, result);
        }

        [Fact]
        public void shouldGetExtraTimeValueFromNullPrice()
        {
            var entryCommand = new EntryCommand();
            var parkingSessionCommand = new ParkingSession(entryCommand, Guid.NewGuid());

            var result = parkingSessionCommand.GetExtraTimeValue();

            Assert.Equal(0, result);
        }
    }
}
