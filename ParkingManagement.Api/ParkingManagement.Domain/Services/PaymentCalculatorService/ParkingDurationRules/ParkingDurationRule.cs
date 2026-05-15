using ParkingManagement.Domain.Entities;

namespace ParkingManagement.Domain.Services.PaymentCalculatorService
{
    public abstract class ParkingDurationRule
    {
        public decimal BaseValue { get; set; }
        public decimal ExtraTimeValue { get; set; }
        public TimeSpan? Duration { get; set; }

        protected ParkingDurationRule(TimeSpan duration, ParkingSession parkingSession)
        {
            BaseValue = parkingSession.GetBaseValue();
            ExtraTimeValue = parkingSession.GetExtraTimeValue();

            Duration = duration;
        }

        public abstract double GetNumberOfHoursToPay();
        public abstract decimal GetTotalPayable();
    }
}
