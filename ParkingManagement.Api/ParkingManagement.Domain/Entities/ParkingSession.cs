using ParkingManagement.Domain.Commands;

namespace ParkingManagement.Domain.Entities
{
    public class ParkingSession : BaseEntity
    {
        public string LicensePlate { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public Guid PriceId { get; set; }
        public Price Price { get; set; }
        public bool Finished { get; set; } = false;

        private ParkingSession() { }

        public ParkingSession(EntryCommand entryCommand, Guid priceId)
        {
            LicensePlate = entryCommand.LicensePlate;
            EntryTime = DateTime.UtcNow;
            PriceId = priceId;
        }

        public void Exit()
        {
            ExitTime = DateTime.UtcNow;
            Finished = true;
        }
    }
}
