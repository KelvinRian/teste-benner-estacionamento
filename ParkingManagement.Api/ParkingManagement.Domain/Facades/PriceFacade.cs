using ParkingManagement.Domain.Commands;
using ParkingManagement.Domain.Dtos;
using ParkingManagement.Domain.Entities;
using ParkingManagement.Domain.RepositoryInterfaces;

namespace ParkingManagement.Domain.Facades
{
    public class PriceFacade : IPriceFacade
    {
        private readonly IPriceRepository _priceRepository;

        public PriceFacade(IPriceRepository priceRepository)
        {
            _priceRepository = priceRepository;
        }

        public async Task Create(CreatePriceCommand command)
        {
            var price = new Price(command);
            await _priceRepository.Create(price);
        }

        public async Task Delete(Guid priceId)
        {
            await _priceRepository.Delete(priceId);
        }

        public async Task<IEnumerable<PriceDto>> GetAll()
        {
            var prices = await _priceRepository.GetAll();

            return prices.Select(x => new PriceDto
            {
                Id = x.Id,
                BaseValue = x.BaseValue,
                ExtraTimeValue = x.ExtraTimeValue,
                EffectivePeriodStart = x.EffectivePeriodStart,
                EffectivePeriodEnd = x.EffectivePeriodEnd
            });
        }
    }
}
