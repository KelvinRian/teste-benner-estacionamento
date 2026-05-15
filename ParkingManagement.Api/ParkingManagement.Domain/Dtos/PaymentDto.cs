namespace ParkingManagement.Domain.Dtos
{
    public class PaymentDto
    {
        public TimeSpan Duration { get; set; }
        public decimal PriceBaseValue { get; set; }
        public double NumberOfHoursToPay { get; set; }
        public decimal TotalPayable { get; set; }
    }
}
