namespace ParkingManagement.Domain.Dtos
{
    public class PaymentDto
    {
        public TimeSpan Duration { get; set; }
        public int NumberOfHoursToPay { get; set; }
        public decimal PriceBaseValue { get; set; }
        public decimal TotalPayable { get; set; }
    }
}
