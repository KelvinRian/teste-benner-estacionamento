namespace ParkingManagement.Domain.Commands
{
    public class CreatePriceCommand
    {
        public decimal BaseValue { get; set; }
        public decimal ExtraTimeValue { get; set; }
        public DateTime EffectivePeriodStart { get; set; }
        public DateTime EffectivePeriodEnd { get; set; }
    }
}
