using ParkingManagement.Domain.Commands;
using ParkingManagement.Domain.Dtos;
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

        public Task Create(CreatePriceCommand command)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid priceId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PriceDto>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
