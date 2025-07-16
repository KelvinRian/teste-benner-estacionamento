using ParkingManagement.Domain.Dtos;
using ParkingManagement.Domain.Entities;

namespace ParkingManagement.Domain.Services
{
    public class PaymentCalculatorService : IPaymentCalculatorService
    {
        private static readonly TimeSpan _halfAnHour = TimeSpan.FromMinutes(30);
        private static readonly TimeSpan _oneHour = TimeSpan.FromHours(1);
        private static readonly TimeSpan _toleranceTime = TimeSpan.FromMinutes(10);

        public PaymentDto CalculatePayment(ParkingSession parkingSession)
        {
            var baseValue = parkingSession?.Price?.BaseValue ?? 0;
            var extraTimeValue = parkingSession?.Price?.ExtraTimeValue ?? 0;
            var duration = parkingSession?.ExitTime - parkingSession?.EntryTime;

            double numberOfHoursToPay = GetNumberOfHoursToPay(duration);
            decimal totalPayable = GetTotalPayable(baseValue, extraTimeValue, duration, numberOfHoursToPay);

            var paymentoDto = new PaymentDto()
            {
                Duration = duration.HasValue ? duration.Value : TimeSpan.Zero,
                NumberOfHoursToPay = numberOfHoursToPay,
                PriceBaseValue = baseValue,
                TotalPayable = totalPayable
            };

            return paymentoDto;
        }

        private static double GetNumberOfHoursToPay(TimeSpan? duration)
        {
            var hours = duration.Value.Hours;
            var minutes = duration.Value - TimeSpan.FromHours(hours);

            if (duration <= _halfAnHour)
                return 0.5;
            else if (duration <= _oneHour + _toleranceTime)
                return 1;
            else if (minutes > _toleranceTime)
                return hours++;
            else
                return hours;
        }

        private static decimal GetTotalPayable(decimal baseValue, decimal extraTimeValue, TimeSpan? duration, double numberOfHoursToPay)
        {
            if (duration <= _halfAnHour)
                return baseValue / 2;
            else
                return baseValue + ((decimal)numberOfHoursToPay - 1) * extraTimeValue;
        }
    }
}
