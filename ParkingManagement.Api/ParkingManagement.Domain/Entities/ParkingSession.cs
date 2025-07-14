namespace ParkingManagement.Domain.Entities
{
    public class ParkingSession : BaseEntity
    {
        public string LicensePlate { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime ExitTime { get; set; }
        public Guid PriceId { get; set; }
        public Price Price { get; set; }
    }
}
