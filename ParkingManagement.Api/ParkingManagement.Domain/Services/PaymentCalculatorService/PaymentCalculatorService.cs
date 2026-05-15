using ParkingManagement.Domain.Dtos;
using ParkingManagement.Domain.Entities;
using ParkingManagement.Domain.Utils;

namespace ParkingManagement.Domain.Services.PaymentCalculatorService
{
    public class PaymentCalculatorService : IPaymentCalculatorService
    {
        public PaymentDto CalculatePayment(ParkingSession parkingSession)
        {
            TimeSpan duration = GetDuration(parkingSession);

            ParkingDurationRule parkingDurationRule = GetParkingDuration(duration, parkingSession);

            var paymentoDto = new PaymentDto()
            {
                Duration = duration,
                PriceBaseValue = parkingSession?.GetBaseValue() ?? 0,
                NumberOfHoursToPay = parkingDurationRule.GetNumberOfHoursToPay(),
                TotalPayable = parkingDurationRule.GetTotalPayable()
            };

            return paymentoDto;
        }

        private static TimeSpan GetDuration(ParkingSession parkingSession)
        {
            TimeSpan? duration;

            if (parkingSession?.Finished ?? false)
                duration = parkingSession?.ExitTime - parkingSession?.EntryTime;
            else
                duration = DateTime.UtcNow - parkingSession?.EntryTime;

            return duration.HasValue ? duration.Value : TimeSpan.Zero;
        }

        private static ParkingDurationRule GetParkingDuration(TimeSpan duration, ParkingSession parkingSession)
        {
            if (duration <= TimeValues.HalfAnHour)
                return new HalfAnHourParkingDurationRule(duration, parkingSession);

            else if (duration <= TimeValues.OneHour + TimeValues.ToleranceTime)
                return new OneHourParkingDurationRule(duration, parkingSession);

            else if (duration > TimeValues.OneHour + TimeValues.ToleranceTime)
                return new MoreThanOneHourParkingDurationRule(duration, parkingSession);

            else
                return new DefaultParkingDurationRule(duration, parkingSession);
        }
    }
}
