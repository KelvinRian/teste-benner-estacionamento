using ParkingManagement.Domain.Commands;
using ParkingManagement.Domain.Dtos;

namespace ParkingManagement.Domain.Facades
{
    public interface IPriceFacade
    {
        Task Create(CreatePriceCommand command);
        Task<IEnumerable<PriceDto>> GetAll();
        Task Delete(Guid priceId);
    }
}
