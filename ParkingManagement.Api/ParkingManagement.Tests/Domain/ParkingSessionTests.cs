using ParkingManagement.Domain.Commands;
using ParkingManagement.Domain.Entities;

namespace ParkingManagement.Tests.Domain
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
    }
}
