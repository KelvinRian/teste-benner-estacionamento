using ParkingManagement.Domain.Entities;
using ParkingManagement.Domain.Utils;

namespace ParkingManagement.Domain.Services.PaymentCalculatorService
{
    public class MoreThanOneHourParkingDurationRule : ParkingDurationRule
    {
        public MoreThanOneHourParkingDurationRule(TimeSpan duration, ParkingSession parkingSession) : base(duration, parkingSession)
        {
        }

        public override double GetNumberOfHoursToPay()
        {
            var hours = Duration.Value.Hours;
            var minutes = Duration.Value - TimeSpan.FromHours(hours);

            if (minutes > TimeValues.ToleranceTime)
                return ++hours;
            else
                return hours;
        }

        public override decimal GetTotalPayable()
            => BaseValue + ((decimal)GetNumberOfHoursToPay() - 1) * ExtraTimeValue;
    }
}
