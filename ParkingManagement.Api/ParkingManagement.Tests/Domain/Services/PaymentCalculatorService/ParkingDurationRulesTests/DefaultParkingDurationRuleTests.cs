using ParkingManagement.Domain.Commands;
using ParkingManagement.Domain.Entities;
using ParkingManagement.Domain.Services.PaymentCalculatorService;

namespace ParkingManagement.Tests.Domain.Services.PaymentCalculatorService.ParkingDurationRulesTests
{
    public class DefaultParkingDurationRuleTests
    {
        [Fact]
        public void shouldGetNumberOfHoursToPay()
        {
            var parkingDurationRule = new DefaultParkingDurationRule(TimeSpan.Zero, 
                   new ParkingSession(new EntryCommand(), Guid.NewGuid()));

            var result = parkingDurationRule.GetNumberOfHoursToPay();

            Assert.Equal(0, result);
        }

        [Fact]
        public void shouldGetTotalPayable()
        {
            var parkingDurationRule = new DefaultParkingDurationRule(TimeSpan.Zero,
                   new ParkingSession(new EntryCommand(), Guid.NewGuid()));

            var result = parkingDurationRule.GetTotalPayable();

            Assert.Equal(0, result);
        }
    }
}
