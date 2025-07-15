namespace ParkingManagement.Domain.Dtos
{
    public class ParkingSessionDto
    {
        public Guid Id { get; set; }
        public string LicensePlate { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public TimeSpan Duration { get; set; }
        public int NumberOfHoursToPay { get; set; }
        public decimal PriceBaseValue { get; set; }
        public decimal TotalPayable { get; set; }
    }
}
