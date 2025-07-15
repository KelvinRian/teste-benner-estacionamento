using ParkingManagement.Domain.Commands;
using ParkingManagement.Domain.Dtos;
using ParkingManagement.Domain.Entities;
using ParkingManagement.Domain.RepositoryInterfaces;

namespace ParkingManagement.Domain.Facades
{
    public class ParkingSessionFacade : IParkingSessionFacade
    {
        private IParkingSessionRepository _parkingSessionRepository;
        private IPriceRepository _priceRepository;

        public ParkingSessionFacade(IParkingSessionRepository parkingSessionRepository, IPriceRepository priceRepository)
        {
            _parkingSessionRepository = parkingSessionRepository;
            _priceRepository = priceRepository;
        }

        public async Task Entry(EntryCommand command)
        {
            var applicablePrice = await GetApplicablePrice(command);
            await CreateParkingSession(command, applicablePrice);
        }

        private async Task<Price> GetApplicablePrice(EntryCommand entryCommand)
        {
            var applicablePrice = await _priceRepository.GetLatestApplicableFor(DateTime.UtcNow);
            if (applicablePrice == null)
                throw new ArgumentNullException("Não foi possível realizar a entrada. Tabela de preços está vazia");

            return applicablePrice;
        }

        private async Task CreateParkingSession(EntryCommand entryCommand, Price applicablePrice)
        {
            var parkingSession = new ParkingSession(entryCommand, applicablePrice.Id);
            await _parkingSessionRepository.Create(parkingSession);
        }

        public async Task Exit(Guid parkingSessionId)
        {
            var parkingSession = await GetParkingSession(parkingSessionId);
            await RegisterExitTime(parkingSession);
        }

        private async Task<ParkingSession> GetParkingSession(Guid parkingSessionId)
        {
            var parkingSession = await _parkingSessionRepository.GetById(parkingSessionId);
            if (parkingSession == null)
                throw new ArgumentNullException("Sessão de estacionamento não encontrada");

            return parkingSession;
        }

        private async Task RegisterExitTime(ParkingSession parkingSession)
        {
            parkingSession.Exit();
            await _parkingSessionRepository.Update(parkingSession);
        }

        public Task<IEnumerable<ParkingSessionDto>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
