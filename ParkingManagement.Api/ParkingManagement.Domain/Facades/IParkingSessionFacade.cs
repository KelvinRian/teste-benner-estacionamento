using ParkingManagement.Domain.Commands;
using ParkingManagement.Domain.Dtos;

namespace ParkingManagement.Domain.Facades
{
    public interface IParkingSessionFacade
    {
        Task Entry(EntryCommand command);
        Task Exit(Guid parkingSessionId);
        Task<IEnumerable<ParkingSessionDto>> GetAll();
        Task<ParkingSessionDto> GetByLicensePlate(string licensePlate);
    }
}
