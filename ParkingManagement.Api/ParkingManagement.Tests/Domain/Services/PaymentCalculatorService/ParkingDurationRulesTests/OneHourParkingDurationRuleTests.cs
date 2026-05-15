using ParkingManagement.Domain.Commands;
using ParkingManagement.Domain.Entities;
using ParkingManagement.Domain.Services.PaymentCalculatorService;

namespace ParkingManagement.Tests.Domain.Services.PaymentCalculatorService.ParkingDurationRulesTests
{
    public class OneHourParkingDurationRuleTests
    {
        [Fact]
        public void shouldGetNumberOfHoursToPay()
        {
            var parkingDurationRule = new OneHourParkingDurationRule(TimeSpan.Zero,
                   new ParkingSession(new EntryCommand(), Guid.NewGuid()));

            var result = parkingDurationRule.GetNumberOfHoursToPay();

            Assert.Equal(1, result);
        }

        [Fact]
        public void shouldGetTotalPayable()
        {
            var price = new Price(new CreatePriceCommand() { BaseValue = 10 });
            var parkingSession = new ParkingSession(new EntryCommand(), Guid.NewGuid())
            {
                Price = price
            };

            var parkingDurationRule = new OneHourParkingDurationRule(TimeSpan.Zero, parkingSession);

            var result = parkingDurationRule.GetTotalPayable();

            Assert.Equal(price.BaseValue, result);
        }
    }
}
