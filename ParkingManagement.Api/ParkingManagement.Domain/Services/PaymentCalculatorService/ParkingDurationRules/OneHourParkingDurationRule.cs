using ParkingManagement.Domain.Entities;

namespace ParkingManagement.Domain.Services.PaymentCalculatorService
{
    public class OneHourParkingDurationRule : ParkingDurationRule
    {
        public OneHourParkingDurationRule(TimeSpan duration, ParkingSession parkingSession) : base(duration, parkingSession)
        {
        }

        public override double GetNumberOfHoursToPay()
            => 1;

        public override decimal GetTotalPayable()
            => BaseValue;
    }
}
