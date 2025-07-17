using ParkingManagement.Domain.Entities;

namespace ParkingManagement.Domain.RepositoryInterfaces
{
    public interface IParkingSessionRepository
    {
        Task Create(ParkingSession parkingSession);

        Task Update(ParkingSession parkingSession);

        Task<IEnumerable<ParkingSession>> GetAll();

        Task<ParkingSession> GetById(Guid id);

        Task<ParkingSession> GetByLicensePlate(string licensePlate);
    }
}
