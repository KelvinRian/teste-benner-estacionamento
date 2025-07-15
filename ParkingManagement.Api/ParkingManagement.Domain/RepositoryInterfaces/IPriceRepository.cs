using ParkingManagement.Domain.Entities;

namespace ParkingManagement.Domain.RepositoryInterfaces
{
    public interface IPriceRepository
    {
        Task Create(Price price);

        Task<Price> GetLatestApplicableFor(DateTime parkingSessionEntryDate);

        Task<IEnumerable<Price>> GetAll();

        Task<bool> HasAnyParkingSession(Guid priceId);

        Task Delete(Guid priceId);
    }
}
