namespace ParkingManagement.Domain.Dtos
{
    public class ParkingSessionDto
    {
        public Guid Id { get; set; }
        public string LicensePlate { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public PaymentDto Payment { get; set; }
    }
}
