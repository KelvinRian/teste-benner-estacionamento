
using ParkingManagement.Domain.Entities;

namespace ParkingManagement.Domain.Services.PaymentCalculatorService
{
    public class DefaultParkingDurationRule : ParkingDurationRule
    {
        public DefaultParkingDurationRule(TimeSpan duration, ParkingSession parkingSession) : base(duration, parkingSession)
        {
        }

        public override double GetNumberOfHoursToPay()
            => 0;

        public override decimal GetTotalPayable()
            => 0;
    }
}
