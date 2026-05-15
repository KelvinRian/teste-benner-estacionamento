using ParkingManagement.Domain.Entities;

namespace ParkingManagement.Domain.Services.PaymentCalculatorService
{
    public class HalfAnHourParkingDurationRule : ParkingDurationRule
    {
        public HalfAnHourParkingDurationRule(TimeSpan duration, ParkingSession parkingSession) : base(duration, parkingSession)
        {
        }

        public override double GetNumberOfHoursToPay()
            => 0.5;

        public override decimal GetTotalPayable()
            => BaseValue / 2;
    }
}
