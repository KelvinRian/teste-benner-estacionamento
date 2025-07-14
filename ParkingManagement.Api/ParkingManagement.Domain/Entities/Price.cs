using ParkingManagement.Domain.Commands;

namespace ParkingManagement.Domain.Entities
{
    public class Price : BaseEntity
    {
        public decimal BaseValue { get; private set; }
        public decimal ExtraTimeValue { get; private set; }
        public DateTime EffectivePeriodStart { get; private set; }
        public DateTime EffectivePeriodEnd { get; private set; }
        public DateTime CreationDate { get; private set; }

        private Price() { }

        public Price(CreatePriceCommand createPriceCommand)
        {
            BaseValue = createPriceCommand.BaseValue;
            ExtraTimeValue = createPriceCommand.ExtraTimeValue;
            EffectivePeriodStart = createPriceCommand.EffectivePeriodStart;
            EffectivePeriodEnd = createPriceCommand.EffectivePeriodEnd;
            CreationDate = DateTime.UtcNow;
        }
    }
}
