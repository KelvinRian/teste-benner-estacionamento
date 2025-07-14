namespace ParkingManagement.Domain.Entities
{
    public class Price : BaseEntity
    {
        public decimal BaseValue { get; set; }
        public decimal ExtraTimeValue { get; set; }
        public DateTime EffectivePeriodStart { get; set; }
        public DateTime EffectivePeriodEnd { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
