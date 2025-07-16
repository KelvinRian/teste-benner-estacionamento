using ParkingManagement.Domain.Commands;
using ParkingManagement.Domain.Dtos;
using ParkingManagement.Domain.Entities;
using ParkingManagement.Domain.RepositoryInterfaces;
using ParkingManagement.Domain.Services;

namespace ParkingManagement.Domain.Facades
{
    public class ParkingSessionFacade : IParkingSessionFacade
    {
        private IParkingSessionRepository _parkingSessionRepository;
        private IPriceRepository _priceRepository;
        private IPaymentCalculatorService _paymentCalculatorService;

        public ParkingSessionFacade(IParkingSessionRepository parkingSessionRepository, IPriceRepository priceRepository, IPaymentCalculatorService paymentCalculatorService)
        {
            _parkingSessionRepository = parkingSessionRepository;
            _priceRepository = priceRepository;
            _paymentCalculatorService = paymentCalculatorService;
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

        public async Task<IEnumerable<ParkingSessionDto>> GetAll()
        {
            var parkingSessions = await _parkingSessionRepository.GetAll();

            return parkingSessions.Select(CreateParkingSessionDto);  
        }

        private ParkingSessionDto CreateParkingSessionDto(ParkingSession parkingSession)
        {
            return new ParkingSessionDto
            {
                Id = parkingSession.Id,
                LicensePlate = parkingSession.LicensePlate,
                EntryTime = parkingSession.EntryTime,
                ExitTime = parkingSession.ExitTime,
                Payment = _paymentCalculatorService.CalculatePayment(parkingSession)
            };
        }

        public async Task<ParkingSessionDto> GetByLicensePlate(string licensePlate)
        {
            var parkingSession = await _parkingSessionRepository.GetByLicensePlate(licensePlate);
            if (parkingSession == null)
                return new ParkingSessionDto();

            return CreateParkingSessionDto(parkingSession);
        }
    }
}
