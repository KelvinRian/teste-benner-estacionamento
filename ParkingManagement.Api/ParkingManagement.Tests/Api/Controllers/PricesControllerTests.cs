using NSubstitute;
using ParkingManagement.Api.Controllers;
using ParkingManagement.Domain.Facades;

namespace ParkingManagement.Tests.Api.Controllers
{
    public class PricesControllerTests
    {
        private IPriceFacade _priceFacade;
        private readonly PricesController _controller;

        public PricesControllerTests()
        {
            _priceFacade = Substitute.For<IPriceFacade>();
            _controller = new PricesController(_priceFacade);
        }
    }
}
