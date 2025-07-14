using ParkingManagement.Domain.Entities;
using ParkingManagement.Domain.RepositoryInterfaces;
using ParkingManagement.Infrastructure.Context;

namespace ParkingManagement.Infrastructure.Repositories
{
    public class PriceRepository : IPriceRepository
    {
        private readonly ParkingManagementDbContext _context;

        public PriceRepository(ParkingManagementDbContext parkingManagementDbContext)
        {
            _context = parkingManagementDbContext;
        }

        public Task Create(Price price)
        {
            throw new NotImplementedException();
        }

        public Task Delete()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Price>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Price> GetLatestApplicableFor(DateTime parkingSessionEntryDate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasAnyParkingSession(Guid priceId)
        {
            throw new NotImplementedException();
        }
    }
}
