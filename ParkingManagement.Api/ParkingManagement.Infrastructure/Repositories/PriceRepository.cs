using Microsoft.EntityFrameworkCore;
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

        public async Task Create(Price price)
        {
            await _context.Prices.AddAsync(price);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid priceId)
        {
            var price = _context.Prices.FirstOrDefault(x => x.Id == priceId);
            if (price != null)
            {
                _context.Prices.Remove(price);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Price>> GetAll()
        {
            return await _context.Prices.ToListAsync();
        }

        public async Task<Price> GetLatestApplicableFor(DateTime parkingSessionEntryDate)
        {
            var price = await _context.Prices
                .Where(x => x.EffectivePeriodStart <= parkingSessionEntryDate && x.EffectivePeriodEnd >= parkingSessionEntryDate)
                .OrderByDescending(x => x.CreationDate)
                .FirstOrDefaultAsync();

            if (price == null)
            {
                price = await _context.Prices
                    .OrderByDescending(x => x.EffectivePeriodEnd)
                    .FirstOrDefaultAsync();
            }

            return price;
        }

        public async Task<bool> HasAnyParkingSession(Guid priceId)
        {
            return await _context.ParkingSessions
                .AnyAsync(x => x.PriceId == priceId);
        }
    }
}
