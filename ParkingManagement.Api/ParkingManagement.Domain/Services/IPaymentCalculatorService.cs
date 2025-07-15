using ParkingManagement.Domain.Dtos;

namespace ParkingManagement.Domain.Services
{
    public interface IPaymentCalculatorService
    {
        PaymentDto CalculatePayment(PaymentDto payment);
    }
}
