using Microsoft.EntityFrameworkCore;
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

        public async Task Create(ParkingSession parkingSession)
        {
            await _context.ParkingSessions.AddAsync(parkingSession);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ParkingSession>> GetAll()
        {
            return await _context.ParkingSessions
                .OrderByDescending(x => x.EntryTime)
                .ToListAsync();
        }

        public async Task<ParkingSession> GetById(Guid id)
        {
            return await _context.ParkingSessions
                .Include(x => x.Price)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(ParkingSession parkingSession)
        {
            _context.ParkingSessions.Update(parkingSession);
            await _context.SaveChangesAsync();
        }
    }
}
