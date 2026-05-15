using ParkingManagement.Domain.Commands;
using ParkingManagement.Domain.Entities;
using ParkingManagement.Domain.Services.PaymentCalculatorService;

namespace ParkingManagement.Tests.Domain.Services.PaymentCalculatorService.ParkingDurationRulesTests
{
    public class HalfAnHourParkingDurationRuleTests
    {
        [Fact]
        public void shouldGetNumberOfHoursToPay()
        {
            var parkingDurationRule = new HalfAnHourParkingDurationRule(TimeSpan.Zero,
                   new ParkingSession(new EntryCommand(), Guid.NewGuid()));

            var result = parkingDurationRule.GetNumberOfHoursToPay();

            Assert.Equal(0.5, result);
        }

        [Fact]
        public void shouldGetTotalPayable()
        {
            var price = new Price(new CreatePriceCommand() { BaseValue = 10 });
            var parkingSession = new ParkingSession(new EntryCommand(), Guid.NewGuid()) 
            { 
                Price = price
            };

            var parkingDurationRule = new HalfAnHourParkingDurationRule(TimeSpan.Zero, parkingSession);

            var result = parkingDurationRule.GetTotalPayable();

            var expectedResult = price.BaseValue / 2;
            Assert.Equal(expectedResult, result);
        }
    }
}
