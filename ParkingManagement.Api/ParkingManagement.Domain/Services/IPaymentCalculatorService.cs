using ParkingManagement.Domain.Dtos;
using ParkingManagement.Domain.Entities;

namespace ParkingManagement.Domain.Services
{
    public interface IPaymentCalculatorService
    {
        PaymentDto CalculatePayment(ParkingSession parkingSession);
    }
}
