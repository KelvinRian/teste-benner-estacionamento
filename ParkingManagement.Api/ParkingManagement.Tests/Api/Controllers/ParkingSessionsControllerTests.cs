using NSubstitute;
using ParkingManagement.Api.Controllers;
using ParkingManagement.Domain.Facades;

namespace ParkingManagement.Tests.Api.Controllers
{
    public class ParkingSessionsControllerTests
    {
        private IParkingSessionFacade _sessionFacade;
        private readonly ParkingSessionsController _controller;

        public ParkingSessionsControllerTests()
        {
            _sessionFacade = Substitute.For<IParkingSessionFacade>();
            _controller = new ParkingSessionsController(_sessionFacade);
        }
    }
}
