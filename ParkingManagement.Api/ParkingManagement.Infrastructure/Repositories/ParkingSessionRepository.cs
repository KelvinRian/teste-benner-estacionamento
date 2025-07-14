using ParkingManagement.Domain.Entities;
using ParkingManagement.Domain.RepositoryInterfaces;
using ParkingManagement.Infrastructure.Context;

namespace ParkingManagement.Infrastructure.Repositories
{
    public class ParkingSessionRepository : IParkingSessionRepository
    {
        private ParkingManagementDbContext _context;

        public ParkingSessionRepository(ParkingManagementDbContext parkingManagementDbContext)
        {
            _context = parkingManagementDbContext;
        }

        public Task Create(ParkingSession parkingSession)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ParkingSession>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task Update(ParkingSession parkingSession)
        {
            throw new NotImplementedException();
        }
    }
}
