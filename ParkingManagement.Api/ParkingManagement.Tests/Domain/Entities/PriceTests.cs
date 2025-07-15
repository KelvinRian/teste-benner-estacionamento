using ParkingManagement.Domain.Commands;
using ParkingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagement.Tests.Domain.Entities
{
    public class PriceTests
    {
        [Fact]
        public void shouldConstruct()
        {
            var command = new CreatePriceCommand()
            {
                BaseValue = 10,
                ExtraTimeValue = 5,
                EffectivePeriodStart = DateTime.Now.AddDays(-10),
                EffectivePeriodEnd = DateTime.Now,
            };

            var result = new Price(command);

            Assert.Equal(command.BaseValue, result.BaseValue);
            Assert.Equal(command.ExtraTimeValue, result.ExtraTimeValue);
            Assert.Equal(command.EffectivePeriodStart, result.EffectivePeriodStart);
            Assert.Equal(command.EffectivePeriodEnd, result.EffectivePeriodEnd);
            Assert.InRange(result.CreationDate, DateTime.UtcNow.AddMinutes(-2), DateTime.UtcNow.AddMinutes(2));
        }
    }
}
