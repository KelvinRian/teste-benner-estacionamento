using ParkingManagement.Domain.Commands;
using ParkingManagement.Domain.Entities;
using ParkingManagement.Domain.Services.PaymentCalculatorService;

namespace ParkingManagement.Tests.Domain.Services.PaymentCalculatorService.ParkingDurationRulesTests
{
    public class MoreThanOneHourParkingDurationRuleTests
    {
        [Fact]
        public void shouldGetNumberOfHours()
        {
            var fourHours = TimeSpan.FromHours(4);
            var parkingDurationRule = new MoreThanOneHourParkingDurationRule(fourHours,
                               new ParkingSession(new EntryCommand(), Guid.NewGuid()));

            var result = parkingDurationRule.GetNumberOfHoursToPay();

            Assert.Equal(fourHours.Hours, result);
        }

        [Fact]
        public void shouldGetNumberOfHoursToPayPassingToleranceTime()
        {
            var twoHoursAndAHalf = TimeSpan.FromMinutes(150);
            var parkingDurationRule = new MoreThanOneHourParkingDurationRule(twoHoursAndAHalf,
                   new ParkingSession(new EntryCommand(), Guid.NewGuid()));

            var result = parkingDurationRule.GetNumberOfHoursToPay();

            Assert.Equal(3, result);
        }

        [Fact]
        public void shouldGetTotalPayable()
        {
            var price = new Price(new CreatePriceCommand() { BaseValue = 10, ExtraTimeValue = 50 });
            var parkingSession = new ParkingSession(new EntryCommand(), Guid.NewGuid())
            {
                Price = price
            };

            var parkingDurationRule = new MoreThanOneHourParkingDurationRule(TimeSpan.Zero, parkingSession);

            var result = parkingDurationRule.GetTotalPayable();

            var expectedResult = price.BaseValue + ((decimal)parkingDurationRule.GetNumberOfHoursToPay() - 1) * price.ExtraTimeValue;
            Assert.Equal(expectedResult, result);
        }
    }
}
