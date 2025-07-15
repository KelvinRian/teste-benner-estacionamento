namespace ParkingManagement.Domain.Dtos
{
    public class PriceDto
    {
        public Guid Id { get; set; }
        public decimal BaseValue { get; set; }
        public decimal ExtraTimeValue { get; set; }
        public DateTime EffectivePeriodStart { get; set; }
        public DateTime EffectivePeriodEnd { get; set; }
    }
}
