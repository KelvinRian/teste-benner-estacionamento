using ParkingManagement.Domain.Commands;
using ParkingManagement.Domain.Dtos;
using ParkingManagement.Domain.RepositoryInterfaces;

namespace ParkingManagement.Domain.Facades
{
    public class ParkingSessionFacade : IParkingSessionFacade
    {
        private IParkingSessionRepository _parkingSessionRepository;

        public ParkingSessionFacade(IParkingSessionRepository parkingSessionRepository)
        {
            _parkingSessionRepository = parkingSessionRepository;
        }

        public Task Entry(EntryCommand command)
        {
            throw new NotImplementedException();
        }

        public Task Exit(Guid parkingSessionId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ParkingSessionDto>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
